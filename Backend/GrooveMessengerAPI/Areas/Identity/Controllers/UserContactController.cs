using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Areas.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserContactController> _logger;
        private readonly IConfiguration _config;
        private readonly IUserResolverService _userResolverService;
        private readonly IContactService _userContactService;
        private static readonly HttpClient Client = new HttpClient();
        private readonly IAuthEmailSenderUtil _authEmailSender;

        public UserContactController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<UserContactController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IAuthEmailSenderUtil authEmailSender,
            IContactService userService,
            IUserResolverService userResolverService)

        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _userResolverService = userResolverService;
            _authEmailSender = authEmailSender;
            _userContactService = userService;
        }
        // GET: api/Contact
        [HttpGet]
        public IQueryable<FullContactModel> GetContactsByIdentity()
        {
            var identityUsername = _userResolverService.CurrentUserName();
            return _userContactService.GetFromUsername(identityUsername);
        }

        // GET: api/Contact/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Contact
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Contact/5
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
