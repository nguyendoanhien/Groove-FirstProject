using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using crypto;
using Google.Apis.Auth;
using GrooveMessengerAPI.Areas.Identity.Models;
using GrooveMessengerAPI.Areas.Identity.Models.ModelsSocial;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

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
        private static readonly HttpClient Client = new HttpClient();

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
                   await _signInManager.SignInAsync(resultFindByMail, isPersistent: false);
                  
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

        [HttpPost]
        [Route("loginfacebook")]
        public async Task<ObjectResult> Fblogin(string token)
        {
            var AppId = _config["ApplicationFacebook:AppId"].ToString();
            var AppSecret = _config["ApplicationFacebook:AppSecret"].ToString();

            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={AppId}&client_secret={AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={token}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest("false to login with FB");
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={token}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            ExternalLoginInfo info = new ExternalLoginInfo(null, "Facebook", userInfo.Id, "Facebook");

            var user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    Email = userInfo.Email,
                    DisplayName = userInfo.Name,
                    UserName = userInfo.Email
                };

                var result = await _userManager.CreateAsync(appUser);
                var resultLogin = await _userManager.AddLoginAsync(appUser, info);

            }
            else
            {
                var resultLogin = await _userManager.AddLoginAsync(user, info);
            }
            var refreshToken = AuthTokenUtil.GetJwtTokenString(userInfo.Email, _config);
            return new OkObjectResult(refreshToken);
        }
    }

    
}
