using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Constants;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MessageController : ApiControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMessageService _mesService;
        private readonly HubConnectionStorage _connectionStore;
        private readonly IParticipantService _participantService;
        private readonly IHubContext<MessageHub, IMessageHubClient> _hubContext;
        private readonly IUserService _userService;
        private readonly IConversationService _conversationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(
            IMessageService mesService,
            IParticipantService participantService,
            IContactService contactService,
            IUserResolverService userResolver,
            IHubContext<MessageHub, IMessageHubClient> hubContext,
            IConversationService conversationService,
            HubConnectionStorage connectionStore,
            IUserService userService,
            UserManager<ApplicationUser> userManager
        ) : base(userResolver)
        {
            _participantService = participantService;
            _mesService = mesService;
            _contactService = contactService;
            _hubContext = hubContext;
            _connectionStore = connectionStore;
            _userService = userService;
            _conversationService = conversationService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PagingParameterModel pagingParameterModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentPage = pagingParameterModel.PageNumber;
            var pageSize = pagingParameterModel.PageSize;
            var result = _mesService.LoadMoreMessages(currentPage, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_mesService.GetMessageById(id) != null) return Ok(_mesService.GetMessageById(id));
            return BadRequest();
        }

        [HttpGet("conversation/{id}")]
        public ActionResult<ChatModel> GetMessagesByConversation(Guid id)
        {
            var chatModel = new ChatModel { Id = id.ToString(), Dialog = new List<DialogModel>() };
            var messages = _mesService.GetMessagesByConversation(id);
            foreach (var message in messages)
            {
                var senderInform = _userService.GetUserInfo(message.SenderId);
                var diaglogModel = new DialogModel
                {
                    Id = message.Id,
                    Time = message.CreatedOn,
                    Who = message.SenderId,
                    Message = message.Content,
                    Avatar = senderInform.Avatar,
                    NickName = senderInform.DisplayName
                };
                chatModel.Dialog.Add(diaglogModel);
            }
            return Ok(chatModel);
        }

        [HttpPut("{id}")]
        public EditMessageModel EditMessage(Guid id, [FromBody] EditMessageModel message)
        {
            if (id != message.Id) return null;

            if (ModelState.IsValid)
            {
                var isExisting = _mesService.CheckExisting(id);
                if (!isExisting) return null;

                _mesService.EditMessageModel(message);
                return message;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMessageModel createMessageModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var createdMessage = await _mesService.AddMessageAsync(createMessageModel); // add message to db
            if (createdMessage != null) // broadcast message to user
            {
                Message message = new Message(createdMessage.ConversationId, createdMessage.SenderId, createdMessage.Id, createdMessage.Content, createdMessage.CreatedOn);
                var receiverEmail = await _contactService.GetUserContactEmail(createMessageModel.Receiver);
                foreach (var connectionId in _connectionStore.GetConnections("message", receiverEmail))
                {
                    await _hubContext.Clients.Client(connectionId).SendMessage(message);
                }
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("group")]
        public async Task<IActionResult> SendMessageToGroup([FromBody] CreateMessageModel createMessageModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var createdMessage = await _mesService.AddMessageAsync(createMessageModel); // add message to db
            var senderInform = _userService.GetUserInfo(createMessageModel.SenderId);
            var memberIds = _participantService.GetParticipantUsersByConversation(createdMessage.ConversationId); // get all member id in group
            var conversationInform = _conversationService.getConversation(createMessageModel.ConversationId); // get conversation inform    
            foreach (var id in memberIds)
            {
                var memberInfo = await _userManager.FindByIdAsync(id);
                foreach (var connectionId in _connectionStore.GetConnections(HubConstant.MessageHubTopic, memberInfo.UserName))
                {
                    await _hubContext.Groups.AddToGroupAsync(connectionId, conversationInform.Name);
                }
            }
            if (createdMessage != null) // broadcast message to user
            {
                var message = new MessageInGroup(createdMessage.ConversationId, createdMessage.SenderId, createdMessage.Id,
                    senderInform.DisplayName, senderInform.Avatar, createdMessage.Content, createdMessage.CreatedOn);

                var groupName = _conversationService.GetGroupNameById(message.FromConv);

                await _hubContext.Clients.Group(conversationInform.Name).BroadcastMessageToGroup(message);

                foreach (var id in memberIds)
                {
                    var memberInfo = await _userManager.FindByIdAsync(id);
                    foreach (var connectionId in _connectionStore.GetConnections(HubConstant.MessageHubTopic, memberInfo.UserName))
                    {
                        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, conversationInform.Name);
                    }
                }

                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(Guid id)
        {
            var isExisting = _mesService.CheckExisting(id);
            if (!isExisting) return new NotFoundResult();
            _mesService.DeleteMessage(id);
            return Ok();
        }


        [HttpGet("read/{conversationId}")]
        public IActionResult Get(Guid conversationId)
        {
            if (!ModelState.IsValid) return BadRequest();

            _mesService.SetValueSeenBy(CurrentUserId.ToString(), conversationId);
            foreach (var connectionId in _connectionStore.GetConnections("message", CurrentUserId.ToString()))
            {
                var unreadMessageModel = new UnreadMessageModel { ConversationId = conversationId, Amount = 0 };
                _hubContext.Clients.Client(connectionId).SendUnreadMessagesAmount(unreadMessageModel);
            }

            return Ok();
        }

        //Truc: Get UnreadMessageAmount
        [HttpGet("unread/{conversationId}")]
        public IActionResult GetUnreadMessages(Guid conversationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            List<string> contactsList = _contactService.GetContacts(conversationId,CurrentUserId);
            foreach (var email in contactsList)
                foreach (var connectionId in _connectionStore.GetConnections("message", email))
                {
                    var unreadMessageAmount = _mesService.GetUnreadMessages(conversationId,email);
                    var unreadMessageModel = new UnreadMessageModel
                    { ConversationId = conversationId, Amount = unreadMessageAmount };
                    _hubContext.Clients.Client(connectionId).SendUnreadMessagesAmount(unreadMessageModel);
                }

            return Ok();
        }
    }
}