using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;

        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        ApiUsersService apiUsersService;

        public UsersController(ApiUsersService apiUsersService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            url = "https://api.spoonacular.com/users/connect" + "?apiKey=" + API_KEY;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.apiUsersService = apiUsersService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            /*if (ModelState.IsValid)
            {
                //TODO: convert incoming info to LoginViewModel

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Ok(ModelState);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    return BadRequest(ModelState);
                }
            }*/

            return Ok(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
           /* if (ModelState.IsValid)
            {
                //TODO: convert incoming info to RegisterViewModel

                IdentityUser user = new IdentityUser
                {
                    UserName = name,
                    Email = email
                };

                // добавляем пользователя
                var res = await userManager.CreateAsync(user, model.Password);
                if (res.Succeeded)
                {
                    // установка куки
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

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            /*// удаляем аутентификационные куки
            await signInManager.SignOutAsync();*/
            return Ok(ModelState);
        }
    }
}
