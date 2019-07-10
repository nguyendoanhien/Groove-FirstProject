using GrooveMessengerAPI.Areas.Identity.Models;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Web;

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
        private readonly IAuthEmailSenderUtil _authEmailSender;
        public ClientAccountController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<ClientAccountController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IAuthEmailSenderUtil authEmailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _authEmailSender = authEmailSender;
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
            else
            {
                var user = _userManager.FindByEmailAsync(model.Email);
                if (user.Result == null)
                {
                    var new_user = new ApplicationUser() { DisplayName = model.DisplayName, Email = model.Email, UserName = model.Email };
                    var result = await _userManager.CreateAsync(new_user, model.Password);
                    if (result.Succeeded)
                    {
                        string token = _userManager.GenerateEmailConfirmationTokenAsync(new_user).Result;
                        var url = _config["EmailConfirmationRoute:Url"] + "?token=" + HttpUtility.UrlEncode(token) + "&&userid=" + new_user.Id;
                        _authEmailSender.SendEmail(url, "Registration Confirmation Email", model.Email, _config);
                        return Ok(url);
                    }
                    else
                    {
                        _logger.LogError(new_user.UserName + " can not be found", user);
                        return NotFound();
                    }
                }
                else
                {
                    _logger.LogError("Email " + model.Email + "already exists", model);
                    return Content("Email " + model.Email + " already exists");
                }
            }
        }
        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]EmailConfirmationModel model)
        {
            if (model.UserId == null || model.Ctoken == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            model.Ctoken = HttpUtility.UrlDecode(model.Ctoken);
            var res = await _userManager.ConfirmEmailAsync(user, model.Ctoken);
            if (res.Succeeded)
            {
                return Ok("Success");
            }
            else
            {
                _logger.LogError("Invalid Email Content");
                return Content("Invalid Email Content");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel data)
        {

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(data.Username, data.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                var tokenString = AuthTokenUtil.GetJwtTokenString(data.Username, _config);
                return new ObjectResult(tokenString);
            }
            return Unauthorized();
        }
    }
}
