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
    [Route("api/ingredients-expense")]
    [ApiController]
    public class IngredientsExpensesController : ControllerBase
    {
        IngredientsExpensesService ingredientsExpenseService;

        public IngredientsExpensesController(IngredientsExpensesService ingredientsExpensesService)
        {
            this.ingredientsExpenseService = ingredientsExpensesService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<IngredientsExpenseDTO>> Get()
        {
            var expenses = await ingredientsExpenseService.GetAllIngredientsExpensesAsync();
            return expenses;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IngredientsExpenseDTO> Get(int id)
        {
            var expense = await ingredientsExpenseService.GetIngredientsExpenseByIdAsync(id);
            return expense;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<IngredientsExpenseDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var expenses = await ingredientsExpenseService.GetIngredientsExpensesByUserIdAsync(gId);
            return expenses;
        }

        [Authorize]
        [HttpGet("ingr-base-info/{infoId}/user/{userId}")]
        public async Task<IngredientsExpenseDTO> GetByIngredientBaseInfoId(int infoId, string userId)
        {
            var gId = Guid.Parse(userId);

            var expense = await ingredientsExpenseService.GetIngredientsExpenseByIngredientBaseInfoIdAsync(infoId, gId);
            return expense;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientsExpenseDTO = JsonConvert.DeserializeObject<IngredientsExpenseDTO>(jsonObject.ToString());
                await this.ingredientsExpenseService.CreateNewIngredientsExpenseAsync(ingredientsExpenseDTO);

                return Ok(ingredientsExpenseDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var ingredientsExpenseDTO = JsonConvert.DeserializeObject<IngredientsExpenseDTO>(jsonObject.ToString());
                await this.ingredientsExpenseService.UpdateIngredientsExpenseAsync(ingredientsExpenseDTO);

                return Ok(ingredientsExpenseDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.ingredientsExpenseService.GetIngredientsExpenseByIdAsync(id).Result != null)
            {
                await this.ingredientsExpenseService.DeleteIngredientsExpenseAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
