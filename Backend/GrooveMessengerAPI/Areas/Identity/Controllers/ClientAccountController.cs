
using Google.Apis.Auth;
using GrooveMessengerAPI.Areas.Identity.Models;
using GrooveMessengerAPI.Areas.Identity.Models.ModelsSocial;
using GrooveMessengerAPI.Auth;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using GrooveMessengerAPI.Controllers;

namespace GrooveMessengerAPI.Areas.IdentityServer.Controllers
{
    [Route("Identity/[controller]")]
    [ApiController]
    public class ClientAccountController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ClientAccountController> _logger;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private static readonly HttpClient Client = new HttpClient();
        private readonly IAuthEmailSenderUtil _authEmailSender;


        public ClientAccountController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<ClientAccountController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IAuthEmailSenderUtil authEmailSender,
            IUserService userService
            )

        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _authEmailSender = authEmailSender;
            _userService = userService;
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
                var new_user = new ApplicationUser() { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(new_user, model.Password);
                CreateUserInfoModel userInfo = new CreateUserInfoModel();
                userInfo.UserId = new_user.Id;
                userInfo.DisplayName = model.DisplayName;
                _userService.AddUserInfo(userInfo);
                if (result.Succeeded)
                {
                    var clientAppUrl = _config.GetSection("Client").Value;
                    string token = _userManager.GenerateEmailConfirmationTokenAsync(new_user).Result;
                    // TODO: add temp log to diagnose issue on email confirmation, remove if all fine
                    _logger.LogError("Email confirm token: " + token);
                    var url = _config["EmailConfirmationRoute:Url"] + "?ctoken=" + HttpUtility.UrlEncode(token) + "&userid=" + new_user.Id;
                    var body = "<h1>Confirm Your Email</h1>" +
                        "<h3>Hello " + model.DisplayName + " ! </h3>" +
                        "<h3>Tap the button below to confirm your email address.</h3>" +
                        "<h3>If you didn't create an account with <a href='" + clientAppUrl + "'>Groove Messenger</a>, you can safely delete this email.</h3>" +
                        "<table border='0' cellpadding='0' cellspacing='0' width='40% ' style='background-color:#324FEA; border:1px solid #324FEA; border-radius:5px;'>" +
                        "<tr><td align = 'center' valign = 'middle' style = 'color:#ffffff; font-family:Helvetica, Arial, sans-serif; font-size:20px; font-weight:bold; line-height:150%; padding-top:15px; padding-right:30px; padding-bottom:15px; padding-left:30px;'>" +
                        "<a href = '" + url + "' target = '_blank' style = 'color:#ffffff; text-decoration:none;display:block' > Click to verify email </a></td></tr></table> ";
                    _authEmailSender.SendEmail(body, "Registration Confirmation Email", model.Email, _config);
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
            //TODO: add temp log to diagnose issue on email confirmation, remove if all fine
            _logger.LogError("UserId to confirm email: " + model.UserId);
            if (model.UserId == null || model.Ctoken == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            model.Ctoken = HttpUtility.UrlDecode(model.Ctoken);
            // TODO: add temp log to diagnose issue on email confirmation, remove if all fine
            _logger.LogError("Email confirm token: " + model.Ctoken);
            var res = await _userManager.ConfirmEmailAsync(user, model.Ctoken);
            if (res.Succeeded)
            {
                var userInfoModel = _userService.GetUserInfo(user.Id);
                var tokenString = AuthTokenUtil.GetJwtTokenString(user, userInfoModel, _config);
                return Content(tokenString);
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
            if (user == null)
                return BadRequest();

            if (user.PasswordHash == null || !(_userManager.IsEmailConfirmedAsync(user).Result))
                return BadRequest();

            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            var clientAppUrl = _config.GetSection("Client").Value;

            string url = _config["ForgotEmailRoute:Url"] + "?token=" + HttpUtility.UrlEncode(token) + "&&userid=" + user.Id;
            var body = "<h1>Reset Password</h1>" +
                        "< h3 > Hello! </ h3 > " +
                        "<h3>Tap the button below to go reset your password</h3>" +
                        "<h3>If you didn't create an account with <a href='" + clientAppUrl + "'>Groove Messenger</a>, you can safely delete this email.</h3>" +
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
                return BadRequest();
            }
            else
            {
                var validEmail = await _userManager.FindByEmailAsync(model.Email);
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null || validEmail == null)
                {
                    return NotFound();
                }
                if (user.Email != model.Email) { return BadRequest(); }
                var result = await _userManager.ResetPasswordAsync(user, model.Ctoken, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Success");
                    return Content("Resetpassword Success");
                }
                else
                {
                    return BadRequest();
                }
            }
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
                var checkConfirmEmail = await _userManager.IsEmailConfirmedAsync(checkUser);
                if (!checkConfirmEmail)
                {
                    return Unauthorized("Please Comfirm Email");
                }
                _logger.LogInformation("User logged in.");

                var user = await _userManager.FindByNameAsync(data.Username);
                var userInfoModel = _userService.GetUserInfo(user.Id);
                var tokenString = AuthTokenUtil.GetJwtTokenString(user, userInfoModel, _config);
                return new ObjectResult(tokenString);
            }
            return Unauthorized("Email or Password is incorrect");
        }

        [HttpPost]
        [Route("logingoogle")]
        public async Task<ObjectResult> LoginGoogle(string accessToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(accessToken, new GoogleJsonWebSignature.ValidationSettings());
                ExternalLoginInfo info = new ExternalLoginInfo(null, "Google", payload.Subject, "Google");
                var user = await _userManager.FindByEmailAsync(payload.Email);

                
                if (user == null)
                {
                    user = new ApplicationUser { UserName = payload.Email, Email = payload.Email };
                    var resultCreate = await _userManager.CreateAsync(user);
                    CreateUserInfoModel userInfo = new CreateUserInfoModel();
                    userInfo.UserId = user.Id;
                    userInfo.DisplayName = payload.Name;
                    _userService.AddUserInfo(userInfo);
                    if (resultCreate.Succeeded)
                    {
                        var resultLogin = await _userManager.AddLoginAsync(user, info);

                    }
                }
                else
                {
                    var resultFindByLoginExternal = await _userManager.FindByLoginAsync("Google", payload.Subject);
                    if (resultFindByLoginExternal == null)
                    {
                        var resultLogin = await _userManager.AddLoginAsync(user, info);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                    }
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    await _signInManager.SignInAsync(user, isPersistent: false);


                }
                var userInfoModel = _userService.GetUserInfo(user.Id);
                var tokenString = AuthTokenUtil.GetJwtTokenString(user, userInfoModel, _config);
                return new OkObjectResult(tokenString);

            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return BadRequest("Error !");
        }

        [HttpPost]
        [Route("loginfacebook")]
        public async Task<ObjectResult> Fblogin(string token)
        {
            var appId = _config["ApplicationFacebook:AppId"].ToString();
            var appSecret = _config["ApplicationFacebook:AppSecret"].ToString();

            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials");
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
                    UserName = userInfo.Email
                };

                var result = await _userManager.CreateAsync(appUser);
                CreateUserInfoModel userInform = new CreateUserInfoModel();
                userInform.UserId = appUser.Id;
                userInform.DisplayName = userInfo.Name;
                _userService.AddUserInfo(userInform);
                var resultLogin = await _userManager.AddLoginAsync(appUser, info);

            }
            else
            {
                var resultLogin = await _userManager.AddLoginAsync(user, info);
            }
            var userInfoModel = _userService.GetUserInfo(user.Id);
            var tokenString = AuthTokenUtil.GetJwtTokenString(user, userInfoModel, _config);
            return new OkObjectResult(tokenString);
        }

    }

}
