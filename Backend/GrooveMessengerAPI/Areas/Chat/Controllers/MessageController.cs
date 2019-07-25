
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MessageController : ApiControllerBase
    {
        private readonly IMessageService _mesService;
        private readonly IContactService _contactService;
        private IHubContext<MessageHub, IMessageHubClient> _hubContext;
        private HubConnectionStore<string> _connectionStore;      

        public MessageController(
            IMessageService mesService,          
            IContactService contactService,
            IUserResolverService userResolver,
            IHubContext<MessageHub, IMessageHubClient> hubContext,
            HubConnectionStore<string> connectionStore
            ) : base(userResolver)
        {
            _mesService = mesService;         
            _contactService = contactService;
            _hubContext = hubContext;
            _connectionStore = connectionStore;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                var result = _mesService.loadMoreMessages(CurrentPage, PageSize);
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_mesService.GetMessageById(id) != null) return Ok(_mesService.GetMessageById(id));
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public EditMessageModel EditMessage(Guid id, [FromBody] EditMessageModel message)
        {
            if (id != message.Id)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                var isExisting = _mesService.CheckExisting(id);
                if (!isExisting)
                {
                    return null;
                }

                _mesService.EditMessageModel(message);
                return message;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMessageModel createMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var createdMessage = await _mesService.AddMessageAsync(createMessageModel); // add message to db
                if (createdMessage != null) // broadcast message to user
                {
                    Message message = new Message(createdMessage.ConversationId, createdMessage.SenderId, createdMessage.Id, createdMessage.Content, createdMessage.CreatedOn);

                    var receiverEmail = await _contactService.GetUserContactEmail(createMessageModel.Receiver);
                    foreach (var connectionId in _connectionStore.GetConnections(receiverEmail))
                    {
                        await _hubContext.Clients.Client(connectionId).SendMessage(message);
                    }
                    foreach (var connectionId in _connectionStore.GetConnections(CurrentUserName))
                    {
                        await _hubContext.Clients.Client(connectionId).SendMessage(message);
                    }
                    return Ok();
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(Guid id)
        {
            var isExisting = _mesService.CheckExisting(id);
            if (!isExisting)
            {
                return new NotFoundResult();
            }

            _mesService.DeleteMessage(id);
            return Ok();
        }


        [HttpGet("read/{conversationId}")]
        public IActionResult Get(Guid conversationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _mesService.SetValueSeenBy(CurrentUserId.ToString(), conversationId);
                foreach (var connectionId in _connectionStore.GetConnections(CurrentUserId.ToString()))
                {
                    UnreadMessageModel unreadMessageModel = new UnreadMessageModel() { ConversationId = conversationId, Amount = 0 };
                    _hubContext.Clients.Client(connectionId).SendUnreadMessagesAmount(unreadMessageModel);
                }
                return Ok();
            }
        }

        //Truc: Get UnreadMessageAmount
        [HttpGet("unread/{conversationId}")]
        public async Task<IActionResult> GetUnreadMessages(Guid conversationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                //var result = _mesService.GetUnreadMessages(conversationId);
                var contactsList = await _contactService.GetContacts(conversationId);

                foreach (var contact in contactsList)
                {
                    foreach (var connectionId in _connectionStore.GetConnections(contact.UserName))
                    {
                        int unreadMessageAmount = _mesService.GetUnreadMessages(conversationId, contact.Id);
                        UnreadMessageModel unreadMessageModel = new UnreadMessageModel() { ConversationId = conversationId, Amount = unreadMessageAmount };

                        await _hubContext.Clients.Client(connectionId).SendUnreadMessagesAmount(unreadMessageModel);
                    }
                }
                return Ok();
            }
        }
    }

}