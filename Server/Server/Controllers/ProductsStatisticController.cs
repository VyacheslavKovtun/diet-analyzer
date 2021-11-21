using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Infrastructure.Business.DTO;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/product-statistic")]
    [ApiController]
    public class ProductsStatisticController : ControllerBase
    {
        ProductsStatisticService productsStatisticService;

        public ProductsStatisticController(ProductsStatisticService productsStatisticsService)
        {
            this.productsStatisticService = productsStatisticsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ProductStatisticDTO>> Get()
        {
            var statistic = await productsStatisticService.GetAllProductsStatisticAsync();
            return statistic;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ProductStatisticDTO> Get(int id)
        {
            var statistic = await productsStatisticService.GetProductStatisticByIdAsync(id);
            return statistic;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsStatisticDTO = JsonConvert.DeserializeObject<ProductStatisticDTO>(jsonObject.ToString());
                await this.productsStatisticService.CreateNewProductStatisticAsync(productsStatisticDTO);

                return Ok(productsStatisticDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsStatisticDTO = JsonConvert.DeserializeObject<ProductStatisticDTO>(jsonObject.ToString());
                await this.productsStatisticService.UpdateProductStatisticAsync(productsStatisticDTO);

                return Ok(productsStatisticDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.productsStatisticService.GetProductStatisticByIdAsync(id).Result != null)
            {
                await this.productsStatisticService.DeleteProductStatisticAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
