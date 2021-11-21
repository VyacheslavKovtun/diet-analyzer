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
    [Route("api/current-product")]
    [ApiController]
    public class CurrentProductsController : ControllerBase
    {
        CurrentProductsService currentProductsService;

        public CurrentProductsController(CurrentProductsService currentProductsService)
        {
            this.currentProductsService = currentProductsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CurrentProductDTO>> Get()
        {
            var product = await currentProductsService.GetAllCurrentProductsAsync();
            return product;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CurrentProductDTO> Get(int id)
        {
            var product = await currentProductsService.GetCurrentProductByIdAsync(id);
            return product;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<CurrentProductDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var product = await currentProductsService.GetCurrentProductsByUserIdAsync(gId);
            return product;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var currentProductDTO = JsonConvert.DeserializeObject<CurrentProductDTO>(jsonObject.ToString());
                await this.currentProductsService.CreateNewCurrentProductAsync(currentProductDTO);

                return Ok(currentProductDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var currentProductDTO = JsonConvert.DeserializeObject<CurrentProductDTO>(jsonObject.ToString());
                await this.currentProductsService.UpdateCurrentProductAsync(currentProductDTO);

                return Ok(currentProductDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.currentProductsService.GetCurrentProductByIdAsync(id).Result != null)
            {
                await this.currentProductsService.DeleteCurrentProductAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
