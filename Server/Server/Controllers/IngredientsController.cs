using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Services.Api.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;

        public IngredientsController()
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");
        }

        [HttpGet]
        [Route("search/name/{name}")]
        public IEnumerable<Result> GetByName(string name)
        { 
            url = "https://api.spoonacular.com/food/ingredients/search" + "?apiKey=" + API_KEY + "&query=" + name;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            Root root = JsonConvert.DeserializeObject<Root>(json);
            return root.results;
        }

        [HttpGet]
        [Route("search/id/{id}")]
        public IngredientRoot GetById(int id)
        {
            url = "https://api.spoonacular.com/food/ingredients/" + id + "/information?apiKey=" + API_KEY;

            IngredientRoot root = JsonConvert.DeserializeObject<IngredientRoot>(client.GetStringAsync(url).Result);
            return root;
        }
    }
}
