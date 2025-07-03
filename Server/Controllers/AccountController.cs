using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Models.DTOs.ApplicationUser;
using Server.Models.DTOs;
using Server.Models.Entities;
using Server.Models.Identities;
using Server.Repo.repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtRepository _jwtRepository;
        private readonly JwtOptions _jwtOptions;

        public AccountController(UserManager<ApplicationUser> userManager, JwtRepository jwtRepository, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtRepository = jwtRepository;
            _jwtOptions = jwtOptions;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO userFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = userFromRequest.UserName,
                    Email = userFromRequest.Email,
                    PhoneNumber = userFromRequest.PhoneNumber,
                };
                //applicationUser.UserName = userFromRequest.UserName;
                //applicationUser.Email = userFromRequest.Email;
                //applicationUser.PhoneNumber = userFromRequest.PhoneNumber;

                //Save Db
                IdentityResult result = await _userManager.CreateAsync(applicationUser, userFromRequest.Password);
                if (result.Succeeded)
                {
                    return Created();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }

            }
            return BadRequest(ModelState); // NotFound

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO userFromRequest)
        {

            if (ModelState.IsValid)
            {
                //check 
                ApplicationUser userFromDb = await _userManager.FindByEmailAsync(userFromRequest.Email);
                if (userFromDb != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userFromDb, userFromRequest.Password);
                    if (found)
                    {
                        // generate Token
                        string token = await _jwtRepository.GenerateToken(userFromDb);
                        return Ok(new
                        {
                            access_token = token,
                            expires_in_minutes = _jwtOptions.Lifetime,
                            user = new
                            {
                                userFromDb.Id,
                                userFromDb.UserName,
                                userFromDb.Email
                            }
                        });
                    }
                }
            }
            ModelState.AddModelError("Email", "Email Or Password is Wrong");
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpGet("check-user")]
        public IActionResult CheckUser()
        {
            var user = HttpContext.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                return Ok($"Logged in as: {user.Identity.Name} and {HttpClient.DefaultProxy.Credentials.GetCredential}");
            }
            return Unauthorized("You're not authenticated");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgot)
        {

            if (ModelState.IsValid)
            {
                // step 1 check if user Exists
                ApplicationUser applicationUser = await _userManager.FindByEmailAsync(forgot.Email);
                if (applicationUser == null)
                {
                    // Don't tell the user that the Email doesn't Exist
                    return Ok("If The Email Exists, A Password Reset Link has been send");
                }

                // step 2 Generate reset Token
                var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);

                // step 3 Encoding Token to Avoid special Characters like + / = 

                var encodedToken = WebUtility.UrlEncode(token);

                // step 4 constructs The Reset link
                var resetUrl = $"http://localhost:4200/reset-password?email={forgot.Email}&token={encodedToken}";

                Console.WriteLine("Reset Link" + resetUrl);

                return Ok("If The Email Exists ,a reset link has been Sent");
            }
            return BadRequest(ModelState);

        }

        //step 2 after ForgotPassword
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByEmailAsync(model.Email);
                if (applicationUser == null)
                    return BadRequest("Invalid Email");

                var decodedToken = WebUtility.UrlDecode(model.Token);

                IdentityResult result = await _userManager.ResetPasswordAsync(applicationUser, decodedToken, model.NewPassword);

                if (result.Succeeded)
                    return Ok("Password Has Been Reset Successfully!");

                foreach (var error in result.Errors)
                {
                    if (error.Code == "InvalidToken")
                    {
                        ModelState.AddModelError("Token", "Your Token link may have expired, please request a new one ");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        // Change password of Already Signed in User 
        [Authorize]
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //_userManager.GetUserAsync(User);
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);

                IdentityResult result = await _userManager.ChangePasswordAsync(applicationUser, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Password has been changed Successfully!");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }


    }
}
