using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Infrastructure.Business.DTO;
using Server.Services.Api.Products;
using Server.Services.Interfaces.Services;
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

        ApiUserDTO apiUser { get; set; }

        ForbiddenProductsService forbiddenProductsService;
        ApiUsersService apiUsersService;
        ProductsBaseInfoService productsBaseInfoService;
        UserManager<IdentityUser> userManager;

        public ProductsController(ForbiddenProductsService forbiddenProductsService, ApiUsersService apiUsersService,
            UserManager<IdentityUser> userManager, ProductsBaseInfoService productsBaseInfoService)
        {
            API_KEY = Environment.GetEnvironmentVariable("SPOONACULAR_API_KEY");

            this.forbiddenProductsService = forbiddenProductsService;
            this.apiUsersService = apiUsersService;
            this.productsBaseInfoService = productsBaseInfoService;
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

        private async Task<List<string>> GetForbiddenProductsList()
        {
            FillCurrentUserInfo();
            var forbiddenProductsList = await this.forbiddenProductsService.GetForbiddenProductsByUserIdAsync(this.apiUser.Id);
            List<string> forbiddenProducts = new List<string>();

            foreach (var item in forbiddenProductsList)
            {
                var bInfo = await this.productsBaseInfoService.GetProductBaseInfoByIdAsync(item.ProductId);
                forbiddenProducts.Add(bInfo.Title);
            }

            return forbiddenProducts;
        }

        [HttpGet]
        [Route("search/title/{title}")]
        public IEnumerable<Product> GetByTitle(string title)
        {
            var forbiddenProducts = GetForbiddenProductsList().Result;

            url = "https://api.spoonacular.com/food/products/search" + "?apiKey=" + API_KEY + "&query=" + title;
            var json = JsonConvert.DeserializeObject(client.GetStringAsync(url).Result).ToString();

            Root root = JsonConvert.DeserializeObject<Root>(json);

            var result = root.products.Where(p => !forbiddenProducts.Contains(p.title));

            return result;
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
