using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiControllerBase
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public ContactController(
            IContactService contactService,
            IUserResolverService userResolver,
            IUserService userService
        )
            : base(userResolver)
        {
            _contactService = contactService;
            _userResolverService = UserResolverService;
            _userService = userService;
        }

        [HttpGet("getallcontactinform")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactService.GetUserContactList());
        }

        [HttpGet("getallunknowncontactinform")]
        public async Task<IActionResult> GetUnknown([FromQuery] PagingParameterModel pagingparametermodel)
        {
            return Ok(await _contactService.GetUserUnknownContact(displayNameSearch: pagingparametermodel.SearchKey));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(Guid id)
        {
            try
            {
                _contactService.DeleteContact(id);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] AddContactModel addContactModel)
        {
            try
            {
                _contactService.AddContact(addContactModel);

                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditContact(Guid id, [FromBody] EditContactModel editContactModel)
        {
            if (_contactService.GetSingle(id) == null) return BadRequest("Failed");
                _contactService.EditContact(editContactModel);
                return Ok("Success");
        }

        [HttpGet("getchatlist")]
        public IActionResult GetChatList()
        {
            return Ok(_contactService.GetLatestContactChatListByUserId_SP());
            
        }
    }
}