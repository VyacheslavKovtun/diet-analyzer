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
    [Route("api/ingredient-statistic")]
    [ApiController]
    public class IngredientsStatisticController : ControllerBase
    {
        IngredientsStatisticService ingredientsStatisticService;

        public IngredientsStatisticController(IngredientsStatisticService ingredientsStatisticsService)
        {
            this.ingredientsStatisticService = ingredientsStatisticsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<IngredientStatisticDTO>> Get()
        {
            var statistic = await ingredientsStatisticService.GetAllIngredientsStatisticAsync();
            return statistic;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IngredientStatisticDTO> Get(int id)
        {
            var statistic = await ingredientsStatisticService.GetIngredientStatisticByIdAsync(id);
            return statistic;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientsStatisticDTO = JsonConvert.DeserializeObject<IngredientStatisticDTO>(jsonObject.ToString());
                await this.ingredientsStatisticService.CreateNewIngredientStatisticAsync(ingredientsStatisticDTO);

                return Ok(ingredientsStatisticDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientsStatisticDTO = JsonConvert.DeserializeObject<IngredientStatisticDTO>(jsonObject.ToString());
                await this.ingredientsStatisticService.UpdateIngredientStatisticAsync(ingredientsStatisticDTO);

                return Ok(ingredientsStatisticDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.ingredientsStatisticService.GetIngredientStatisticByIdAsync(id).Result != null)
            {
                await this.ingredientsStatisticService.DeleteIngredientStatisticAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
