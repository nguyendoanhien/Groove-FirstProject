using GrooveMessengerAPI.Controllers;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ConversationController : ControllerBase
    //{
    //    private readonly IConversationService _conService;
    //    public ConversationController(IConversationService conService)
    //    {
    //        this._conService = conService;
    //    }

    //    [HttpGet("dialogs/{UserId}")]
    //    public IActionResult GetAll(string UserId)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var rs = _conService.GetAllConversationOfAUser(UserId);
    //            return Ok(rs);
    //        }
    //        return BadRequest();
    //    }

    //    [HttpGet("dialog/{ConversationId}")]
    //    public IActionResult Get(string ConversationId)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var rs = _conService.GetConversationOfAUser(ConversationId);
    //            return Ok(rs);
    //        }
    //        return BadRequest();          
    //    }
    //    // POST: api/Conversation
    //    [HttpPost]
    //    public void Post()
    //    {

    //        _conService.AddConversation();
    //    }
    //}
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


        public ConversationController(IConversationService conService, IMessageService messageService, IParticipantService participantService,
            UserManager<ApplicationUser> userManager, IContactService contactService, IUserService userService,
        IUserResolverService userResolver
            ) : base(userResolver)
        {
            _conService = conService;
            _messageService = messageService;
            _participantService = participantService;
            _userManager = userManager;
            _contactService = contactService;
            _userService = userService;
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

            CreateMessageModel createMessageModel = new CreateMessageModel() { Content = "say hi", SenderId = userIndexcurrent.UserId, Type = "text", ConversationId = createConversationModel.Id };
            _messageService.AddMessage(createMessageModel);


            //// create participant
            ParticipantModel par = new ParticipantModel() { Id = Guid.NewGuid(), UserId = userIndex.UserId, ConversationId = createConversationModel.Id, Status = 1 };
            _participantService.AddParticipant(par);
            ParticipantModel parcurrent = new ParticipantModel() { Id = Guid.NewGuid(), UserId = user.Id, ConversationId = createConversationModel.Id, Status = 1 };
            _participantService.AddParticipant(parcurrent);

            var messageRes = new { who = createMessageModel.SenderId, message = createMessageModel.Content, time = DateTime.UtcNow };
   
            return new ObjectResult(new { message = messageRes, conversation = createConversationModel, contact = userIndex });
        }
    }
}