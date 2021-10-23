using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public UsersController()
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            url = "https://api.spoonacular.com/users/connect" + "?apiKey=" + API_KEY;
        }

        [HttpPost]
        public string Post()
        {
            var values = new Dictionary<string, string>
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
            return str;
        }
    }
}
