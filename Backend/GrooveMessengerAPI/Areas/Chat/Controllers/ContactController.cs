﻿using GrooveMessengerAPI.Controllers;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ContactController : ApiControllerBase
    {
        private IContactService _contactService;

        public ContactController(
            IContactService contactService,
            IUserResolverService userResolver) 
            : base(userResolver)
        {
            _contactService = contactService;
        }

        [HttpGet("getallcontactinform")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactService.GetAllContact());
        }
        [HttpGet("getchatlist")]
        public async Task<IActionResult> GetChatList()
        {
            return Ok(await _contactService.GetLatestContactChatListByUserId());
        }
    }
}
