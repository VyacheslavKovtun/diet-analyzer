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
    }
}
