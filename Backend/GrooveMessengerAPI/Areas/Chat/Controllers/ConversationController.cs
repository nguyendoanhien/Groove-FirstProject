using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conService;
        public ConversationController(IConversationService conService)
        {
            _conService = conService;
        }

       // GET: api/Conversation
       //[HttpGet("{username}")]
       // public async Task<IActionResult> Get(string username)
       // {
       //     if (ModelState.IsValid)
       //     {
       //         return Ok(await _conService.GetConversations(username));
       //     }
       //     return BadRequest();
       // }

        //GET: api/Conversation/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }



        // POST: api/Conversation
        [HttpPost]
        public void Post()
        {

            _conService.AddConversation();
        }

        // PUT: api/Conversation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}