
using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Models;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;


namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : ApiControllerBase
    {
        private readonly IMessageService _mesService;
        private readonly IConversationService _conService;
        //private IHubContext<MessageHub, ITypedHubClient> _hubContext;
        //private IMessageService _messageService;

        public MessageController(
            IMessageService mesService,
            IConversationService conService,
            IUserResolverService userResolver
            ) : base(userResolver)
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

        [HttpPut("{id}")]
        public EditMessageModel EditMessage(Guid id, [FromBody] EditMessageModel message)
        {
            if (id != message.Id)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                var isExisting = _mesService.CheckExisting(id);
                if (!isExisting)
                {
                    return null;
                }

                _mesService.EditMessageModel(message);
                return message;
            }

            return null;
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(Guid id)
        {
            var isExisting = _mesService.CheckExisting(id);
            if (!isExisting)
            {
                return new NotFoundResult();
            }

            _mesService.DeleteMessage(id);
            return Ok();
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
        public async Task<ActionResult<IndexMessageModel>> Post([FromBody] CreateMessageModel createMessageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {               
                return await _mesService.AddMessageAsync(createMessageModel);
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