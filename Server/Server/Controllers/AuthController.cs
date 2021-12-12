using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Infrastructure.Business.DTO;
using Server.Models;
using Server.Services;
using Server.Services.Api.ConnectingUserResponse;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;

        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        ApiUsersService apiUsersService;

        public AuthController(ApiUsersService apiUsersService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            url = "https://api.spoonacular.com/users/connect" + "?apiKey=" + API_KEY;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.apiUsersService = apiUsersService;
        }

        [HttpGet]
        [Route("isAuthed")]
        public bool GetIsAuthed()
        {
            var isAuthed = signInManager.IsSignedIn(this.User);
            return isAuthed;
        }

        [Authorize]
        [HttpGet]
        [Route("current-user")]
        public async Task<ApiUserDTO> GetCurrentUser()
        {
            var identityUser = await userManager.GetUserAsync(this.User);
            if (identityUser != null)
            {
                var gId = Guid.Parse(identityUser.Id);

                var apiUser = await this.apiUsersService.GetApiUserByIdAsync(gId);

                return apiUser;
            }
            else return null;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Not confirmed email");
                        return BadRequest(model);
                    }
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                
                if(result.Succeeded)
                {
                    return new JsonResult(new
                    {
                        id = user.Id,
                        name = user.UserName
                    });
                }
                else
                {
                    return BadRequest(new { errorText = "Invalid login or password." });
                }
            }
            else return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var res = await userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "auth",
                        "api",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailSender emailSender = new EmailSender();
                    await emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        $"Confirm your email by following link: <a href='{callbackUrl}'>link</a>");

                    var values = new Dictionary<string, string>
                    {
                        {"username", model.UserName },
                        {"firstName", "Api" },
                        {"lastName", "User" },
                        {"email", model.Email }
                    };

                    var content = JsonConvert.SerializeObject(values);
                    var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                    var response = client.PostAsync(url, httpContent).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        var str = response.Content.ReadAsStringAsync().Result;
                        ConnectingUserResponse connectedUser = JsonConvert.DeserializeObject<ConnectingUserResponse>(str);

                        var newApiUser = await userManager.FindByEmailAsync(model.Email);

                        await apiUsersService.CreateNewApiUserAsync(new Infrastructure.Business.DTO.ApiUserDTO
                        {
                            Id = Guid.Parse(newApiUser.Id),
                            Username = connectedUser.username,
                            ApiPassword = connectedUser.spoonacularPassword,
                            Hash = connectedUser.hash
                        });

                        return new JsonResult(new
                        {
                            successful = true
                        });
                    }
                    else
                    {
                        return new JsonResult(new
                        {
                            successful = false
                        });
                    }
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return BadRequest();
        }

        [Authorize]
        [HttpDelete]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(ModelState);
        }
    }
}
