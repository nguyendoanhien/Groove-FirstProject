using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ApiControllerBase
    {
        private IContactService _contactService;
        private readonly IUserService _userService;
        private readonly IUserResolverService _userResolverService;
        private readonly IHubContext<ContactHub, IContactHubClient> _contactHubContext;
        public ContactController(
            IContactService contactService,
            IUserResolverService userResolver, 
            IUserService userService,
            IHubContext<ContactHub, IContactHubClient> contactHubContext
          )
            : base(userResolver)
        {
            _contactService = contactService;
            _userResolverService = userResolverService;
            _userService = userService;
            _contactHubContext = contactHubContext;

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

    }
}
