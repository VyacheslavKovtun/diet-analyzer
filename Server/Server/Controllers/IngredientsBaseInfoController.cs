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
    [Route("api/ingredient-base-info")]
    [ApiController]
    public class IngredientsBaseInfoController : ControllerBase
    {
        IngredientsBaseInfoService ingredientBaseInfoService;

        public IngredientsBaseInfoController(IngredientsBaseInfoService ingredientBaseInfosService)
        {
            this.ingredientBaseInfoService = ingredientBaseInfosService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<IngredientBaseInfoDTO>> Get()
        {
            var info = await ingredientBaseInfoService.GetAllIngredientsBaseInfoAsync();
            return info;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IngredientBaseInfoDTO> Get(int id)
        {
            var info = await ingredientBaseInfoService.GetIngredientBaseInfoByIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpGet("api/{id}")]
        public async Task<IngredientBaseInfoDTO> GetByApiId(int id)
        {
            var info = await ingredientBaseInfoService.GetIngredientBaseInfoByApiIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientBaseInfoDTO = JsonConvert.DeserializeObject<IngredientBaseInfoDTO>(jsonObject.ToString());
                await this.ingredientBaseInfoService.CreateNewIngredientBaseInfoAsync(ingredientBaseInfoDTO);

                return Ok(ingredientBaseInfoDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientBaseInfoDTO = JsonConvert.DeserializeObject<IngredientBaseInfoDTO>(jsonObject.ToString());
                await this.ingredientBaseInfoService.UpdateIngredientBaseInfoAsync(ingredientBaseInfoDTO);

                return Ok(ingredientBaseInfoDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.ingredientBaseInfoService.GetIngredientBaseInfoByIdAsync(id).Result != null)
            {
                await this.ingredientBaseInfoService.DeleteIngredientBaseInfoAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
