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
    [Route("api/forbidden-product")]
    [ApiController]
    public class ForbiddenProductsController : ControllerBase
    {
        ForbiddenProductsService forbiddenProductsService;

        public ForbiddenProductsController(ForbiddenProductsService forbiddenProductsService)
        {
            this.forbiddenProductsService = forbiddenProductsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ForbiddenProductDTO>> Get()
        {
            var product = await forbiddenProductsService.GetAllForbiddenProductsAsync();
            return product;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ForbiddenProductDTO> Get(int id)
        {
            var product = await forbiddenProductsService.GetForbiddenProductByIdAsync(id);
            return product;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ForbiddenProductDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var products = await forbiddenProductsService.GetForbiddenProductsByUserIdAsync(gId);
            return products;
        }

        [Authorize]
        [HttpGet("product-base-info/{prodId}/user/{userId}")]
        public async Task<ForbiddenProductDTO> GetByProdBaseInfoId(int prodId, string userId)
        {
            var gId = Guid.Parse(userId);

            var product = await forbiddenProductsService.GetForbiddenProductByProdBaseInfoIdAsync(prodId, gId);
            return product;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var forbiddenProductDTO = JsonConvert.DeserializeObject<ForbiddenProductDTO>(jsonObject.ToString());
                await this.forbiddenProductsService.CreateNewForbiddenProductAsync(forbiddenProductDTO);

                return Ok(forbiddenProductDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var forbiddenProductDTO = JsonConvert.DeserializeObject<ForbiddenProductDTO>(jsonObject.ToString());
                await this.forbiddenProductsService.UpdateForbiddenProductAsync(forbiddenProductDTO);

                return Ok(forbiddenProductDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.forbiddenProductsService.GetForbiddenProductByIdAsync(id).Result != null)
            {
                await this.forbiddenProductsService.DeleteForbiddenProductAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
