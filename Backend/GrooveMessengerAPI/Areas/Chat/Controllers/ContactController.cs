using GrooveMessengerAPI.Controllers;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiControllerBase
    {
        private IContactService _contactService;
        private readonly IUserResolverService _userResolverService;
        public ContactController(
            IContactService contactService,

            IUserResolverService userResolver)
            : base(userResolver)
        {
            _contactService = contactService;
            _userResolverService = userResolverService;
        }

        [HttpGet("getallcontactinform")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactService.GetUserContactList());
        }
        [HttpGet("getallunknowncontactinform")]
        public async Task<IActionResult> GetUnknown()
        {
            return Ok(await _contactService.GetUserUnknownContact());
        }
        [HttpDelete("deleteactactinform")]

        public IActionResult DeleteContact([FromBody]DeleteContactModel deleteContactModel)
        {
           
            try
            {
                 _contactService.DeleteContact(deleteContactModel);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }

        }

        [HttpPost("addContact")]
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

        [HttpPut("editContact")]
        public IActionResult EditContact(string contactId, [FromBody] EditContactModel editContactModel)
        {
          

            if (contactId != editContactModel.ContactId) return BadRequest();
            

            try
            {

                _contactService.EditContact(editContactModel);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }
        }

        [HttpGet("getchatlist")]
        public IActionResult GetChatList()
        {
            return Ok(_contactService.GetLatestContactChatListByUserId());
        }
    }
}
