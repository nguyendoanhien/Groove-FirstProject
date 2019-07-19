using GrooveMessengerDAL.Services.Interface;
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
            this._conService = conService;
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
        // POST: api/Conversation
        [HttpPost]
        public void Post()
        {

            _conService.AddConversation();
        }
    }
}