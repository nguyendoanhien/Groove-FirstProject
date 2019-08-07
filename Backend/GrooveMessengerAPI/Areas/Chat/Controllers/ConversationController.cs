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
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using DAL = GrooveMessengerDAL.Models.PagingModel;
namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ApiControllerBase
    {
        private readonly IConversationService _conService;
        private readonly IHubContext<ContactHub, IContactHubClient> _contactHubContext;
        private readonly IContactService _contactService;
        private readonly IMessageService _messageService;
        private readonly IParticipantService _participantService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly HubConnectionStorage _hubConnectionStore;
        private readonly IConfiguration _config;

        public ConversationController(IConversationService conService, IMessageService messageService,
            IParticipantService participantService,
            UserManager<ApplicationUser> userManager, IContactService contactService, IUserService userService,
            IUserResolverService userResolver, IHubContext<ContactHub, IContactHubClient> contactHubContext,
            HubConnectionStorage hubConnectionStore,
            IConfiguration config
        ) : base(userResolver)
        {
            _conService = conService;
            _messageService = messageService;
            _participantService = participantService;
            _userManager = userManager;
            _contactService = contactService;
            _userService = userService;
            _contactHubContext = contactHubContext;
            _hubConnectionStore = hubConnectionStore;
            _config = config;
        }

        [HttpGet("dialogs/{UserId}")]
        public IActionResult GetAll(string userId)
        {
            if (userId == "undefined") return Ok();
            if (ModelState.IsValid)
            {
                var rs = _conService.GetAllConversationOfAUser(CurrentUserId.ToString());
                return Ok(rs);
            }
            return BadRequest();
        }

        [HttpGet("dialog/{ConversationId}")]
        public IActionResult Get(string conversationId, [FromQuery] DAL.PagingParameterModel pagingparametermodel)
        {
            if (ModelState.IsValid)
            {
                var rs = _conService.GetConversationById(conversationId, pagingparametermodel);
                return Ok(rs);
            }

            return BadRequest();
        }

        [HttpPost("addconversation")]
        public IActionResult Post([FromBody] CreateConversationModel createMessageModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            _conService.AddConversation(createMessageModel);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> CreateNewConversation(IndexUserInfoModel userIndex)
        {
            var user = await _userManager.FindByEmailAsync(CurrentUserName);


            // get current userinfo 

            var userIndexcurrent = _userService.GetUserInfo(user.Id);

            // add contact userindex->usercurrent
            var contact = new AddContactModel
                {UserId = userIndexcurrent.Id.ToString(), NickName = userIndex.DisplayName, ContactId = userIndex.Id};
            _contactService.AddContact(contact);

            // add contact usercurrent ->userindex
            var contactcurrent = new AddContactModel
            {
                UserId = userIndex.Id.ToString(), NickName = userIndexcurrent.DisplayName,
                ContactId = userIndexcurrent.Id
            };
            _contactService.AddContact(contactcurrent);

            // create conversation
            var createConversationModel = new CreateConversationModel
            {
                Id = Guid.NewGuid(), Avatar = $"{_config.GetSection("Server").Value}/images/avatar.png", Name = userIndex.DisplayName
            };
            _conService.AddConversation(createConversationModel);

            var createMessageModel = new CreateMessageModel
            {
                Content = "Hello " + userIndex.DisplayName + ". Nice to meet you!", SenderId = userIndexcurrent.UserId,
                Type = "text", ConversationId = createConversationModel.Id
            };
            _messageService.AddMessage(createMessageModel);


            //// create participant
            var par = new ParticipantModel
            {
                Id = Guid.NewGuid(), UserId = userIndex.UserId, ConversationId = createConversationModel.Id, Status = 1
            };
            _participantService.AddParticipant(par);
            var parcurrent = new ParticipantModel
                {Id = Guid.NewGuid(), UserId = user.Id, ConversationId = createConversationModel.Id, Status = 1};
            _participantService.AddParticipant(parcurrent);


            var diaglogModel = new List<DialogModel>();
            diaglogModel.Add(new DialogModel
                {Who = createMessageModel.SenderId, Message = createMessageModel.Content, Time = DateTime.UtcNow});
            var dialog = new {id = createConversationModel.Id, dialog = diaglogModel};
            var chatContact = new ContactChatList
            {
                ConvId = createConversationModel.Id.ToString(), ContactId = userIndex.UserId,
                DisplayName = userIndex.DisplayName, LastMessage = createMessageModel.Content,
                LastMessageTime = DateTime.UtcNow
            };

            var chatContactToSend = new ContactChatList
            {
                ConvId = createConversationModel.Id.ToString(), ContactId = userIndexcurrent.UserId,
                DisplayName = userIndexcurrent.DisplayName, LastMessage = createMessageModel.Content,
                LastMessageTime = DateTime.UtcNow
            };

            var contactEmail = await _userManager.FindByIdAsync(userIndex.UserId);

            foreach (var connectionId in _hubConnectionStore.GetConnections(HubConstant.ContactHubTopic, contactEmail.Email))
                await _contactHubContext.Clients.Client(connectionId)
                    .SendNewContactToFriend(userIndexcurrent, chatContactToSend, dialog);

            return new ObjectResult(new {Contact = userIndex, ChatContact = chatContact, dialog});
        }


        [HttpGet("group")]
        public async Task<ActionResult<IEnumerable<GroupConversationModel>>> GetGroups()
        {
            var requestUserInform = await _userManager.FindByNameAsync(CurrentUserName);
            var groupConversations = new List<GroupConversationModel>();
            var convs = _conService.GetGroupConversationsByUsername(CurrentUserName).Result;
            foreach (var conv in convs)
            {
                var groupConversation = new GroupConversationModel();
                groupConversation.Members = new List<MemberInformModel>();
                var membersInConv = _participantService.GetParticipantUsersByConversation(conv.Id);
                foreach (var member in membersInConv)
                {
                    var memberInformContact = _userService.GetUserInfo(member);
                    var memberInformModel = new MemberInformModel
                    {
                        Id = memberInformContact.UserId,
                        Avatar = memberInformContact.Avatar,
                        DisplayName = memberInformContact.DisplayName
                    };
                    groupConversation.Members.Add(memberInformModel);
                }
                var lastestMessage = _messageService.GetLastestMessagebyConversation(conv.Id);
                groupConversation.Id = conv.Id;
                groupConversation.Name = conv.Name;
                groupConversation.Avatar = conv.Avatar;
                groupConversation.UnreadMessage = _messageService.GetUnreadMessages(conv.Id, requestUserInform.UserName);
                if (lastestMessage.Type == "Image") { groupConversation.LastestMessage = "[Image]"; } else groupConversation.LastestMessage = lastestMessage.Content;
                groupConversation.LastestMessageTime = lastestMessage.CreatedOn;
                groupConversations.Add(groupConversation);
            }
            return Ok(groupConversations);
        }

        [HttpPost("group")]
        public async Task<ActionResult> CreateNewConversationGroup([FromBody] InitialGroupModel model)
        {
            var user = await _userManager.FindByEmailAsync(CurrentUserName);
            // Create a new group conversation
            var newConversation = new CreateConversationModel { Id = Guid.NewGuid(), Name = model.Name, Avatar = model.Avatar, IsGroup = true };
            _conService.AddConversation(newConversation);
            // Add creator to new group conversation
            var newCreator = new ParticipantModel
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ConversationId = newConversation.Id,
                Status = 1
            };
            _participantService.AddParticipant(newCreator);
            // Add message to new group conversation
            var createMessageModel = new CreateMessageModel
            {
                Content = "Hello " + model.Name,
                SenderId = newCreator.UserId,
                Type = "text",
                ConversationId = newConversation.Id
            };
            _messageService.AddMessage(createMessageModel);
            var newGroupInfo = new GroupConversationModel
            {
                Id = newConversation.Id,
                Name = newConversation.Name,
                Avatar = newConversation.Avatar,
                Members = new List<MemberInformModel>()
            };
            // Add members to new group conversation 
            foreach (var member in model.Members)
            {
                var memberInformContact = _userService.GetUserInfo(member.UserId);
                var memberInformModel = new MemberInformModel
                {
                    Id = member.UserId,
                    Avatar = memberInformContact.Avatar,
                    DisplayName = memberInformContact.DisplayName
                };
                newGroupInfo.Members.Add(memberInformModel);
                var newMember = new ParticipantModel
                {
                    Id = Guid.NewGuid(),
                    UserId = member.UserId,
                    ConversationId = newConversation.Id,
                    Status = 1
                };
                _participantService.AddParticipant(newMember);
                var memberInfo = await _userManager.FindByIdAsync(newMember.UserId);
                foreach (var connectionId in _hubConnectionStore.GetConnections(HubConstant.ContactHubTopic, memberInfo.UserName))
                {
                    await _contactHubContext.Groups.AddToGroupAsync(connectionId, newConversation.Name);
                }
            }
            await _contactHubContext.Clients.Group(newConversation.Name).BroadcastNewGroupToFriends(newGroupInfo);
            return Ok(newGroupInfo);
        }

        [HttpPut]
        public async Task<IActionResult> EditConversation(EditConversationModel editConversation)
        {
            _conService.editConversation(editConversation);
            foreach (var member in editConversation.Members)
            {
                var memberInfo = await _userManager.FindByIdAsync(member.Id.ToString());
                foreach (var connectionId in _hubConnectionStore.GetConnections(HubConstant.ContactHubTopic, memberInfo.UserName))
                {
                    await _contactHubContext.Groups.AddToGroupAsync(connectionId, editConversation.Name);
                }
                await _contactHubContext.Clients.Group(editConversation.Name).EditConversationToFriends(editConversation);
            }
            return Ok();
        }
    }
}