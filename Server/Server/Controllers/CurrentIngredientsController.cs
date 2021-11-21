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
    [Route("api/current-ingredient")]
    [ApiController]
    public class CurrentIngredientsController : ControllerBase
    {
        CurrentIngredientsService currentIngredientsService;

        public CurrentIngredientsController(CurrentIngredientsService currentIngredientsService)
        {
            this.currentIngredientsService = currentIngredientsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CurrentIngredientDTO>> Get()
        {
            var ingredient = await currentIngredientsService.GetAllCurrentIngredientsAsync();
            return ingredient;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CurrentIngredientDTO> Get(int id)
        {
            var ingredient = await currentIngredientsService.GetCurrentIngredientByIdAsync(id);
            return ingredient;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<CurrentIngredientDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var ingredient = await currentIngredientsService.GetCurrentIngredientsByUserIdAsync(gId);
            return ingredient;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var currentIngredientDTO = JsonConvert.DeserializeObject<CurrentIngredientDTO>(jsonObject.ToString());
                await this.currentIngredientsService.CreateNewCurrentIngredientAsync(currentIngredientDTO);

                return Ok(currentIngredientDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var currentIngredientDTO = JsonConvert.DeserializeObject<CurrentIngredientDTO>(jsonObject.ToString());
                await this.currentIngredientsService.UpdateCurrentIngredientAsync(currentIngredientDTO);

                return Ok(currentIngredientDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.currentIngredientsService.GetCurrentIngredientByIdAsync(id).Result != null)
            {
                await this.currentIngredientsService.DeleteCurrentIngredientAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
