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
    [Route("api/favourite-recipe")]
    [ApiController]
    public class FavouriteRecipesController : ControllerBase
    {
        FavouriteRecipesService favouriteRecipesService;

        public FavouriteRecipesController(FavouriteRecipesService favouriteRecipesService)
        {
            this.favouriteRecipesService = favouriteRecipesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<FavouriteRecipeDTO>> Get()
        {
            var recipe = await favouriteRecipesService.GetAllFavouriteRecipesAsync();
            return recipe;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<FavouriteRecipeDTO> Get(int id)
        {
            var recipe = await favouriteRecipesService.GetFavouriteRecipeByIdAsync(id);
            return recipe;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<FavouriteRecipeDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var recipe = await favouriteRecipesService.GetFavouriteRecipesByUserIdAsync(gId);
            return recipe;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var favouriteRecipeDTO = JsonConvert.DeserializeObject<FavouriteRecipeDTO>(jsonObject.ToString());
                await this.favouriteRecipesService.CreateNewFavouriteRecipeAsync(favouriteRecipeDTO);

                return Ok(favouriteRecipeDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var favouriteRecipeDTO = JsonConvert.DeserializeObject<FavouriteRecipeDTO>(jsonObject.ToString());
                await this.favouriteRecipesService.UpdateFavouriteRecipeAsync(favouriteRecipeDTO);

                return Ok(favouriteRecipeDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.favouriteRecipesService.GetFavouriteRecipeByIdAsync(id).Result != null)
            {
                await this.favouriteRecipesService.DeleteFavouriteRecipeAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
