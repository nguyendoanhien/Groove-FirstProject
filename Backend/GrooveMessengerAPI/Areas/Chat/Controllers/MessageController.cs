using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _mesService;
        private readonly IConversationService _conService;
        //private IHubContext<MessageHub, ITypedHubClient> _hubContext;

        public MessageController(
            //IHubContext<MessageHub, ITypedHubClient> hubContext
            IMessageService mesService,
            IConversationService conService
            )
        {
            //_hubContext = hubContext;
            this._mesService = mesService;
            _conService = conService;
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

        [HttpGet]
        public IActionResult Get([FromQuery]PagingParameterModel pagingparametermodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                int CurrentPage = pagingparametermodel.pageNumber;
                int PageSize = pagingparametermodel.pageSize;
                var result = _mesService.loadMoreMessages(CurrentPage, PageSize);
                return Ok(result);
            }
        }


        [HttpPost("addmessage")]
        public IActionResult Post([FromBody] CreateMessageModel createMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _mesService.AddMessage(createMessageModel);
                return Ok();
            }
        }

        [HttpPut("updatestatusmessage")]
        public IActionResult Put(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _mesService.UpdateStatusMessage(id);
                return Ok();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (_mesService.GetMessageById(id) != null) return Ok(_mesService.GetMessageById(id));
            else return BadRequest();
        }
    }

}