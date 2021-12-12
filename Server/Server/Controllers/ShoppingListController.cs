using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Services.Api.ShoppingList;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/shopping-list")]
    [Authorize]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;
        private string HASH;
        private string USER_NAME;

        ApiUsersService apiUsersService;
        UserManager<IdentityUser> userManager;

        public ShoppingListController(ApiUsersService apiUsersService, UserManager<IdentityUser> userManager)
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            this.apiUsersService = apiUsersService;
            this.userManager = userManager;
        }

        public void FillCurrentUserInfo()
        {
            if (this.User != null)
            {
                var identityUser = this.userManager.GetUserAsync(this.User).Result;
                if (identityUser != null)
                {
                    var gId = Guid.Parse(identityUser.Id);

                    var apiUser = this.apiUsersService.GetApiUserByIdAsync(gId).Result;
                    HASH = apiUser.Hash;
                    USER_NAME = apiUser.Username;
                }
            }
        }

        [HttpGet]
        public IEnumerable<Aisle> Get()
        {
            FillCurrentUserInfo();
            url = "https://api.spoonacular.com/mealplanner/" + USER_NAME + "/shopping-list" + "?hash=" + HASH + "&apiKey=" + API_KEY;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            ShoppingList root = JsonConvert.DeserializeObject<ShoppingList>(json);
            return root.aisles;
        }

        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                FillCurrentUserInfo();
                url = "https://api.spoonacular.com/mealplanner/" + USER_NAME + "/shopping-list/items" + "?hash=" + HASH + "&apiKey=" + API_KEY;

                var content = jsonObject.ToString();
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(url, httpContent);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            FillCurrentUserInfo();
            url = "https://api.spoonacular.com/mealplanner/" + USER_NAME + "/shopping-list/items/" + id + "?hash=" + HASH + "&apiKey=" + API_KEY;

            await client.DeleteAsync(url);

            return Ok();
        }
    }
}
