using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Services.Api;
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

        public RecipesController()
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");
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
            url = "https://api.spoonacular.com/recipes/complexSearch" + "?apiKey=" + API_KEY + "&titleMatch=" + title;
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
