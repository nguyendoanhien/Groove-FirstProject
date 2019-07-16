using System;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        //private IHubContext<MessageHub, ITypedHubClient> _hubContext;

        public MessageController(
            //IHubContext<MessageHub, ITypedHubClient> hubContext
            )
        {
            //_hubContext = hubContext;
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
    }

}