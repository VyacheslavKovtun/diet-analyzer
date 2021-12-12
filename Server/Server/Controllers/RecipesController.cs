using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Infrastructure.Business.DTO;
using Server.Services.Api.Recipes;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;

        ApiUserDTO apiUser { get; set; }

        ForbiddenIngredientsService forbiddenIngredientsService;
        ApiUsersService apiUsersService;
        IngredientsBaseInfoService ingredientsBaseInfoService;
        UserManager<IdentityUser> userManager;

        public RecipesController(ForbiddenIngredientsService forbiddenIngredientsService, ApiUsersService apiUsersService,
            UserManager<IdentityUser> userManager, IngredientsBaseInfoService ingredientsBaseInfoService)
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            this.forbiddenIngredientsService = forbiddenIngredientsService;
            this.apiUsersService = apiUsersService;
            this.ingredientsBaseInfoService = ingredientsBaseInfoService;
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

                    this.apiUser = this.apiUsersService.GetApiUserByIdAsync(gId).Result;
                }
            }
        }

        private async Task<string> GenerateForbiddenIngredientsString()
        {
            FillCurrentUserInfo();
            var forbiddenIngredientsList = await this.forbiddenIngredientsService.GetForbiddenIngredientsByUserIdAsync(this.apiUser.Id);
            string resStr = "";

            foreach(var item in forbiddenIngredientsList)
            {
                var bInfo = await this.ingredientsBaseInfoService.GetIngredientBaseInfoByIdAsync(item.IngredientId);
                resStr += bInfo.Name + ",";
            }

            return resStr;
        }

        [HttpGet]
        [Route("random/{amount}")]
        public IEnumerable<FullRecipe> GetRand(int amount)
        {
            url = "https://api.spoonacular.com/recipes/random" + "?apiKey=" + API_KEY + "&number=" + amount;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            RecipesRoot root = JsonConvert.DeserializeObject<RecipesRoot>(json);

            return root.recipes;
        }

        [HttpGet]
        [Route("search/title/{title}")]
        public IEnumerable<Result> GetByTitle(string title)
        {
            var forbiddenIngredientsParams = GenerateForbiddenIngredientsString().Result;
            url = "https://api.spoonacular.com/recipes/complexSearch" + "?apiKey=" + API_KEY + "&titleMatch=" + title
                + "&excludeIngredients=" + forbiddenIngredientsParams;

            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            Root root = JsonConvert.DeserializeObject<Root>(json);
            return root.results;
        }

        [HttpGet]
        [Route("search/id/{id}")]
        public RecipeRoot GetById(int id)
        { 
            url = "https://api.spoonacular.com/recipes/" + id + "/information?apiKey=" + API_KEY;

            RecipeRoot root = JsonConvert.DeserializeObject<RecipeRoot>(client.GetStringAsync(url).Result);
            return root;
        }
    }
}
