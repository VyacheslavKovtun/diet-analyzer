using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<IActionResult> Post()
        {
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

           /* if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("index", "Home");
            }*/

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
    }
}
