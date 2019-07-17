using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        //private IHubContext<MessageHub, ITypedHubClient> _hubContext;
        private IMessageService _messageService;

        public MessageController(
             //IHubContext<MessageHub, ITypedHubClient> hubContext
             IMessageService messageService
            )
        {
            //_hubContext = hubContext;
            _messageService = messageService;
        }

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage = string.Empty;

            try
            {
                //_hubContext.Clients.All.BroadcastMessage(msg.Type, msg.Payload);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }


        [HttpGet("{id}")]
        public async Task<EditMessageModel> GetMessage(Guid id)
        {
            var data = await _messageService.GetMessageForEditAsync(id);
            return data;
        }

        [HttpPut("{id}")]
        public EditMessageModel EditMessage(Guid id, [FromBody] EditMessageModel message)
        {
            if (id != message.Id)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                var isExisting = _messageService.CheckExisting(id);
                if (!isExisting)
                {
                    return null;
                }

                _messageService.EditMessageModel(message);
                return message;
            }

            return null;
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(Guid id)
        {
            var isExisting = _messageService.CheckExisting(id);
            if (!isExisting)
            {
                return new NotFoundResult();
            }

            _messageService.DeleteMessage(id);
            return Ok();
        }
    }

}