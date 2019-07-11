using System.Net;
using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Identity.Models;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Areas.IdentityServer.Controllers
{
    [Route("Indentity/[controller]")]
    [ApiController]
    public class ClientAccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClientAccountController> _logger;
        private readonly IConfiguration _config;

        public ClientAccountController(SignInManager<ApplicationUser> signInManager, ILogger<ClientAccountController> logger, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
        }
        [HttpGet]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]EmailConfirmationModel model)
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel data)
        {
            
            var checkUser = await _userManager.FindByNameAsync(data.Username);
            if (checkUser == null)
            {
                return Unauthorized("Your username hasn't joined our system, please click Sign Up link to register");
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(data.Username, data.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                var tokenString = AuthTokenUtil.GetJwtTokenString(data.Username, _config);
                return new ObjectResult(tokenString);
            }
            return Unauthorized("Password is incorrect");
        }
    }
}
