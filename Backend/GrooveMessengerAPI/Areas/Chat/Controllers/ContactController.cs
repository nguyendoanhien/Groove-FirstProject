﻿
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
    public class ContactController : Controller
    {
        private IContactService _contactService;
        private readonly IUserResolverService _userResolverService;
        public ContactController(
            IContactService contactService,
            IUserResolverService userResolverService
            )
        {
            _contactService = contactService;
            _userResolverService = userResolverService;
        }

        [HttpGet("getallcontactinform")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactService.GetAllContact());
        }
        [HttpGet("getallunknowncontactinform")]
        public async Task<IActionResult> GetUnknown()
        {
            return Ok(await _contactService.GetAllUnknownContact());
        }
        [HttpDelete("deleteactactinform")]
        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContact(string contactId)
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
    }
}
