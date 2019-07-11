using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using crypto;
using Google.Apis.Auth;
using GrooveMessengerAPI.Areas.Identity.Models;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
        [HttpPost]
        [Route("logingoogle")]
        public async Task<ObjectResult> LoginGoogle(string accessToken)
        {
            try
            {
                //SimpleLogger.Log("userView = " + userView.tokenId);
                var payload = GoogleJsonWebSignature.ValidateAsync(accessToken, new GoogleJsonWebSignature.ValidationSettings()).Result;
                //var user = await _authService.Authenticate(payload);

                ExternalLoginInfo info = new ExternalLoginInfo(null, "Google", payload.Subject, "Google");
                var resultFindByMail = await _userManager.FindByEmailAsync(payload.Email);
                var resultFindByLoginExternal = await _userManager.FindByLoginAsync("Google", payload.Subject);
                var tokenString = AuthTokenUtil.GetJwtTokenString(payload.Email, _config);

                var user = new ApplicationUser { UserName = payload.Email, Email = payload.Email, DisplayName = payload.Name };
                if (resultFindByMail == null)
                {
                    //var info = await _signInManager.GetExternalLoginInfoAsync();

                    var resultCreate = await _userManager.CreateAsync(user);
                    if (resultCreate.Succeeded)
                    {
                        var resultLogin = await _userManager.AddLoginAsync(user, info);
                       
                        return new OkObjectResult(tokenString);
                    }
                }
                else
                {
                    if (resultFindByLoginExternal == null)
                    {
                        var resultLogin = await _userManager.AddLoginAsync(resultFindByMail, info);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                  
                        //_logger.LogInformation("User logged in.");
                        //return LocalRedirect(returnUrl);

                       
                 
                }

          
                return new OkObjectResult(tokenString);

                //SimpleLogger.Log(payload.ExpirationTimeSeconds.ToString());


            }
            catch (Exception ex)
            {
                //Helpers.SimpleLogger.Log(ex);
                BadRequest(ex.Message);
            }
            return BadRequest("Error !");
        }
        //public async Task<ApplicationUser> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        //{
        //    await Task.Delay(1);
        //    return this.FindUserOrAdd(payload);
        //}

        //private ApplicationUser FindUserOrAdd(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        //{
        //    var u = _users.Where(x => x.email == payload.Email).FirstOrDefault();
        //    if (u == null)
        //    {
        //        u = new ApplicationUser()
        //        {
        //            id = Guid.NewGuid(),
        //            name = payload.Name,
        //            email = payload.Email,
        //            oauthSubject = payload.Subject,
        //            oauthIssuer = payload.Issuer
        //        };
        //        _users.Add(u);
        //    }
        //    this.PrintUsers();
        //    return u;
        //}
    }
}
