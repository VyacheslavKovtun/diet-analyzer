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
    [Route("api/product-base-info")]
    [ApiController]
    public class ProductsBaseInfoController : ControllerBase
    {
        ProductsBaseInfoService productsBaseInfoService;

        public ProductsBaseInfoController(ProductsBaseInfoService productsBaseInfosService)
        {
            this.productsBaseInfoService = productsBaseInfosService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ProductBaseInfoDTO>> Get()
        {
            var info = await productsBaseInfoService.GetAllProductsBaseInfoAsync();
            return info;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ProductBaseInfoDTO> Get(int id)
        {
            var info = await productsBaseInfoService.GetProductBaseInfoByIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpGet("api/{apiId}")]
        public async Task<ProductBaseInfoDTO> GetByApiId(int apiId)
        {
            var info = await productsBaseInfoService.GetProductBaseInfoByApiIdAsync(apiId);
            return info;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsBaseInfoDTO = JsonConvert.DeserializeObject<ProductBaseInfoDTO>(jsonObject.ToString());
                await this.productsBaseInfoService.CreateNewProductBaseInfoAsync(productsBaseInfoDTO);

                return Ok(productsBaseInfoDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsBaseInfoDTO = JsonConvert.DeserializeObject<ProductBaseInfoDTO>(jsonObject.ToString());
                await this.productsBaseInfoService.UpdateProductBaseInfoAsync(productsBaseInfoDTO);

                return Ok(productsBaseInfoDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.productsBaseInfoService.GetProductBaseInfoByIdAsync(id).Result != null)
            {
                await this.productsBaseInfoService.DeleteProductBaseInfoAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
