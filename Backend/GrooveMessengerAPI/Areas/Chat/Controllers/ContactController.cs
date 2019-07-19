using GrooveMessengerAPI.Controllers;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
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
            return Ok(await _contactService.GetUserContact());
        }
        [HttpGet("getallunknowncontactinform")]
        public async Task<IActionResult> GetUnknown()
        {
            return Ok(await _contactService.GetUserUnknownContact());
        }
        [HttpDelete("deleteactactinform")]
       
        public async Task<IActionResult> DeleteContact([FromBody]string contactId)
        {
            try
            {
                await  _contactService.DeleteContact(contactId);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }
            
        }

        [HttpPost("addContact")]
        public async Task<IActionResult> AddContact([FromBody] string contactId)
        {
            try
            {
                await _contactService.AddContact(contactId);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Failed");
            }
        }
        [HttpGet("getchatlist")]
        public async Task<IActionResult> GetChatList()
        {
            return Ok(await _contactService.GetLatestContactChatListByUserId());
        }
    }
}
