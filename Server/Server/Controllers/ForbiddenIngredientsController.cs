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
    [Route("api/forbidden-ingredient")]
    [ApiController]
    public class ForbiddenIngredientsController : ControllerBase
    {
        ForbiddenIngredientsService forbiddenIngredientsService;

        public ForbiddenIngredientsController(ForbiddenIngredientsService forbiddenIngredientsService)
        {
            this.forbiddenIngredientsService = forbiddenIngredientsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ForbiddenIngredientDTO>> Get()
        {
            var ingredient = await forbiddenIngredientsService.GetAllForbiddenIngredientsAsync();
            return ingredient;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ForbiddenIngredientDTO> Get(int id)
        {
            var ingredient = await forbiddenIngredientsService.GetForbiddenIngredientByIdAsync(id);
            return ingredient;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ForbiddenIngredientDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var ingredients = await forbiddenIngredientsService.GetForbiddenIngredientsByUserIdAsync(gId);
            return ingredients;
        }

        [Authorize]
        [HttpGet("ingredient-base-info/{ingrId}/user/{userId}")]
        public async Task<ForbiddenIngredientDTO> GetByIngrBaseInfoId(int ingrId, string userId)
        {
            var gId = Guid.Parse(userId);

            var ingredient = await forbiddenIngredientsService.GetForbiddenIngredientByIngrBaseInfoIdAsync(ingrId, gId);
            return ingredient;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var forbiddenIngredientDTO = JsonConvert.DeserializeObject<ForbiddenIngredientDTO>(jsonObject.ToString());
                await this.forbiddenIngredientsService.CreateNewForbiddenIngredientAsync(forbiddenIngredientDTO);

                return Ok(forbiddenIngredientDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var forbiddenIngredientDTO = JsonConvert.DeserializeObject<ForbiddenIngredientDTO>(jsonObject.ToString());
                await this.forbiddenIngredientsService.UpdateForbiddenIngredientAsync(forbiddenIngredientDTO);

                return Ok(forbiddenIngredientDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.forbiddenIngredientsService.GetForbiddenIngredientByIdAsync(id).Result != null)
            {
                await this.forbiddenIngredientsService.DeleteForbiddenIngredientAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
