using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetUnknown([FromQuery]PagingParameterModel pagingparametermodel)
        {
            return Ok(await _contactService.GetUserUnknownContact(displayNameSearch: pagingparametermodel.SearchKey));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(Guid Id)
        {

            try
            {
                _contactService.DeleteContact(Id);
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



            try
            {

                _contactService.EditContact(editContactModel);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed");
            }
        }


    }
}
