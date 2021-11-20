using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure.Business.DTO;
using Server.Models;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                var user = await userManager.FindByEmailAsync(model.Email);

                if (result.Succeeded)
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
            /*if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = name,
                    Email = email
                };

                var res = await userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Ok(ModelState);
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }*/

            /*var values = new Dictionary<string, string>
            {
                {"username", "VyacheslavW" },
                {"firstName", "Vyacheslav" },
                {"lastName", "Kovtun" },
                {"email", "kovtun.v.work@gmail.com" }
            };

            var content = JsonConvert.SerializeObject(values);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, httpContent).Result;
            var str = response.Content.ReadAsStringAsync().Result;
            return str;*/

            //TODO: check for api response before adding to table

            /*var user = await userManager.FindByEmailAsync("kovtun.v.work@gmail.com");

            await apiUsersService.CreateNewApiUserAsync(new Infrastructure.Business.DTO.ApiUserDTO
            {
                Id = Guid.Parse(user.Id),
                Username = "api-103995-vyacheslavw",
                ApiPassword = "grilledzucchini&19cocoapowder",
                Hash = "557054f80c98f9bd9c82b67ae71d959ea3e8e066"
            });*/

            return Ok();
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
