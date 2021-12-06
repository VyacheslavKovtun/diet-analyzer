using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Services.Api.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        string url;

        private string API_KEY;

        public ProductsController()
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");
        }

        [HttpGet]
        [Route("search/title/{title}")]
        public IEnumerable<Product> GetByTitle(string title)
        {
            url = "https://api.spoonacular.com/food/products/search" + "?apiKey=" + API_KEY + "&query=" + title;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            Root root = JsonConvert.DeserializeObject<Root>(json);
            return root.products;
        }

        [HttpGet]
        [Route("search/id/{id}")]
        public ProductRoot GetById(int id)
        {
            url = "https://api.spoonacular.com/food/products/" + id + "?apiKey=" + API_KEY;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            ProductRoot root = JsonConvert.DeserializeObject<ProductRoot>(json);
            return root;
        }
    }
}
