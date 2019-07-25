using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Hubs.Utils;
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ApiControllerBase
    {
        private readonly IConversationService _conService;
        private readonly IMessageService _messageService;
        private readonly IParticipantService _participantService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactService _contactService;
        private readonly IUserService _userService;
        private readonly IHubContext<ContactHub, IContactHubClient> _contactHubContext;
        private HubConnectionStorage _hubConnectionStore;

        public ConversationController(IConversationService conService, IMessageService messageService, IParticipantService participantService,
            UserManager<ApplicationUser> userManager, IContactService contactService, IUserService userService,
        IUserResolverService userResolver, IHubContext<ContactHub, IContactHubClient> contactHubContext, HubConnectionStorage hubConnectionStore
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
        }

        [HttpGet("dialogs/{UserId}")]
        public IActionResult GetAll(string UserId)
        {
            if (ModelState.IsValid)
            {
                var rs = _conService.GetAllConversationOfAUser(UserId);
                return Ok(rs);
            }
            return BadRequest();
        }

        [HttpGet("dialog/{ConversationId}")]
        public IActionResult Get(string ConversationId)
        {
            if (ModelState.IsValid)
            {
                var rs = _conService.GetConversationOfAUser(ConversationId);
                return Ok(rs);
            }
            return BadRequest();
        }

        [HttpPost("addconversation")]
        public IActionResult Post([FromBody] CreateConversationModel createMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _conService.AddConversation(createMessageModel);
                return Ok();
            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateNewConversation(IndexUserInfoModel userIndex)
        {
            var user = await _userManager.FindByEmailAsync(CurrentUserName);


            // get current userinfo 

            IndexUserInfoModel userIndexcurrent = _userService.GetUserInfo(user.Id);

            // add contact userindex->usercurrent
            AddContactModel contact = new AddContactModel() { UserId = userIndexcurrent.Id.ToString(), NickName = userIndex.DisplayName, ContactId = userIndex.Id };
            _contactService.AddContact(contact);

            // add contact usercurrent ->userindex
            AddContactModel contactcurrent = new AddContactModel() { UserId = userIndex.Id.ToString(), NickName = userIndexcurrent.DisplayName, ContactId = userIndexcurrent.Id };
            _contactService.AddContact(contactcurrent);

            // create conversation
            CreateConversationModel createConversationModel = new CreateConversationModel() { Id = Guid.NewGuid(), Avatar = "https://localhost:44383/images/avatar.png", Name = userIndex.DisplayName };
            _conService.AddConversation(createConversationModel);

            CreateMessageModel createMessageModel = new CreateMessageModel() { Content = "Hello "+ userIndex.DisplayName +". Nice to meet you!", SenderId = userIndexcurrent.UserId, Type = "text", ConversationId = createConversationModel.Id };
            _messageService.AddMessage(createMessageModel);


            //// create participant
            ParticipantModel par = new ParticipantModel() { Id = Guid.NewGuid(), UserId = userIndex.UserId, ConversationId = createConversationModel.Id, Status = 1 };
            _participantService.AddParticipant(par);
            ParticipantModel parcurrent = new ParticipantModel() { Id = Guid.NewGuid(), UserId = user.Id, ConversationId = createConversationModel.Id, Status = 1 };
            _participantService.AddParticipant(parcurrent);


            List<DialogModel> diaglogModel = new List<DialogModel> { };
            diaglogModel.Add(new DialogModel() { Who = createMessageModel.SenderId, Message = createMessageModel.Content, Time = DateTime.UtcNow });
            var dialog = new { id = createConversationModel.Id, dialog = diaglogModel };
            ContactChatList chatContact = new ContactChatList { ConvId = createConversationModel.Id.ToString(), ContactId = userIndex.UserId, DisplayName = userIndex.DisplayName, LastMessage = createMessageModel.Content, LastMessageTime = DateTime.UtcNow };

            ContactChatList chatContactToSend = new ContactChatList { ConvId = createConversationModel.Id.ToString(), ContactId = userIndexcurrent.UserId, DisplayName = userIndexcurrent.DisplayName, LastMessage = createMessageModel.Content, LastMessageTime = DateTime.UtcNow };

            var contactEmail = await _userManager.FindByIdAsync(userIndex.UserId);

            foreach (var connectionId in _hubConnectionStore.GetConnections("message", contactEmail.Email))
            {
                await _contactHubContext.Clients.Client(connectionId).SendNewContactToFriend(userIndexcurrent, chatContactToSend, dialog);

            }

            return new ObjectResult(new { Contact =userIndex, ChatContact = chatContact, dialog });
        }
    }
}