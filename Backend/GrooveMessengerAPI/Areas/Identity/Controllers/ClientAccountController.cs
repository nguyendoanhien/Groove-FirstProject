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
                var new_user = new ApplicationUser() { DisplayName = model.DisplayName, Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(new_user, model.Password);
                if (result.Succeeded)
                {
                    string token = _userManager.GenerateEmailConfirmationTokenAsync(new_user).Result;
                    var url = _config["EmailConfirmationRoute:Url"] + "?ctoken=" + HttpUtility.UrlEncode(token) + "&userid=" + new_user.Id;
                    var body = "<h1>Confirm Your Email</h1>" +
                        "<h3>Hello " + new_user.DisplayName + "</h3>" +
                        "<h3>Tap the button below to confirm your email address.</h3>" +
                        "<h3>If you didn't create an account with <a href='http://localhost:4200'>Groove Messenger</a>, you can safely delete this email.</h3>" +
                        "<table border='0' cellpadding='0' cellspacing='0' width='40% ' style='background-color:#324FEA; border:1px solid #324FEA; border-radius:5px;'>" +
                        "<tr><td align = 'center' valign = 'middle' style = 'color:#ffffff; font-family:Helvetica, Arial, sans-serif; font-size:20px; font-weight:bold; line-height:150%; padding-top:15px; padding-right:30px; padding-bottom:15px; padding-left:30px;'>" +
                        "<a href = '" + url + "' target = '_blank' style = 'color:#ffffff; text-decoration:none;display:block' > Click to verify email </a></td></tr></table> ";
                    _authEmailSender.SendEmail(body , "Registration Confirmation Email", model.Email, _config);
                    return Ok();
                }
                else
                {
                    return BadRequest();
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
                return Ok();
            }
            else
            {
                _logger.LogError("Invalid Email Content");
                return Content("Invalid Email Content");
            }
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> Forgotpassword(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = await _userManager.FindByEmailAsync(email);
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            if (user.PasswordHash == null || !(_userManager.IsEmailConfirmedAsync(user).Result) || user == null)
            {
                return BadRequest();
            }
            string url = _config["ForgotEmailRoute:Url"] + "?token=" + HttpUtility.UrlEncode(token) + "&&userid=" + user.Id;
            var body = "<h1>Confirm Your Email</h1>" +
                        "<h3>Hello " + user.DisplayName + "</h3>" +
                        "<h3>Tap the button below to confirm your email address.</h3>" +
                        "<h3>If you didn't create an account with <a href='http://localhost:4200'>Groove Messenger</a>, you can safely delete this email.</h3>" +
                        "<table border='0' cellpadding='0' cellspacing='0' width='40% ' style='background-color:#324FEA; border:1px solid #324FEA; border-radius:5px;'>" +
                        "<tr><td align = 'center' valign = 'middle' style = 'color:#ffffff; font-family:Helvetica, Arial, sans-serif; font-size:20px; font-weight:bold; line-height:150%; padding-top:15px; padding-right:30px; padding-bottom:15px; padding-left:30px;'>" +
                        "<a href = '" + url + "' target = '_blank' style = 'color:#ffffff; text-decoration:none;display:block' > Click to reset your password </a></td></tr></table> ";
            _authEmailSender.SendEmail(body, "Registration Confirmation Email", email, _config);
            this._authEmailSender.SendEmail(body, "Reset Password", email, _config);
            return Ok();
        }



        [HttpPost("resetpassword")]
        public async Task<IActionResult> Resetpassword([FromBody]ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                string tmp = HttpUtility.UrlDecode(model.Ctoken);
                var validEmail = await _userManager.FindByEmailAsync(model.Email);
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null || validEmail == null)
                {
                    return BadRequest(ModelState);
                }
                if(user.Email != model.Email) { return BadRequest(); }
                var result = await _userManager.ResetPasswordAsync(user, model.Ctoken, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Success");
                    return Content("Resetpassword Success");
                }
                else
                {
                    return BadRequest(ModelState);
                }
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
