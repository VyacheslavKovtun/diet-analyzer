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
    [Route("api/recipe-base-info")]
    [ApiController]
    public class RecipesBaseInfoController : ControllerBase
    {
        RecipesBaseInfoService recipesBaseInfoService;

        public RecipesBaseInfoController(RecipesBaseInfoService recipesBaseInfosService)
        {
            this.recipesBaseInfoService = recipesBaseInfosService;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeBaseInfoDTO>> Get()
        {
            var info = await recipesBaseInfoService.GetAllRecipesBaseInfoAsync();
            return info;
        }

        [HttpGet("{id}")]
        public async Task<RecipeBaseInfoDTO> Get(int id)
        {
            var info = await recipesBaseInfoService.GetRecipeBaseInfoByIdAsync(id);
            return info;
        }

        [HttpGet]
        [Route("api/{id}")]
        public async Task<RecipeBaseInfoDTO> GetByApiId(int id)
        {
            var info = await recipesBaseInfoService.GetRecipeBaseInfoByApiIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var recipesBaseInfoDTO = JsonConvert.DeserializeObject<RecipeBaseInfoDTO>(jsonObject.ToString());
                await this.recipesBaseInfoService.CreateNewRecipeBaseInfoAsync(recipesBaseInfoDTO);

                return Ok(recipesBaseInfoDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var recipesBaseInfoDTO = JsonConvert.DeserializeObject<RecipeBaseInfoDTO>(jsonObject.ToString());
                await this.recipesBaseInfoService.UpdateRecipeBaseInfoAsync(recipesBaseInfoDTO);

                return Ok(recipesBaseInfoDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.recipesBaseInfoService.GetRecipeBaseInfoByIdAsync(id).Result != null)
            {
                await this.recipesBaseInfoService.DeleteRecipeBaseInfoAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
