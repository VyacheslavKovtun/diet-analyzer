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
    [Route("api/products-expense")]
    [ApiController]
    public class ProductsExpensesController : ControllerBase
    {
        ProductsExpensesService productsExpensesService;

        public ProductsExpensesController(ProductsExpensesService productsExpensessService)
        {
            this.productsExpensesService = productsExpensessService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<ProductsExpenseDTO>> Get()
        {
            var expense = await productsExpensesService.GetAllProductsExpensesAsync();
            return expense;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ProductsExpenseDTO> Get(int id)
        {
            var expense = await productsExpensesService.GetProductsExpenseByIdAsync(id);
            return expense;
        }

        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IEnumerable<ProductsExpenseDTO>> GetByUserId(string id)
        {
            var gId = Guid.Parse(id);

            var expense = await productsExpensesService.GetProductsExpensesByUserIdAsync(gId);
            return expense;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsExpensesDTO = JsonConvert.DeserializeObject<ProductsExpenseDTO>(jsonObject.ToString());
                await this.productsExpensesService.CreateNewProductsExpenseAsync(productsExpensesDTO);

                return Ok(productsExpensesDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var productsExpensesDTO = JsonConvert.DeserializeObject<ProductsExpenseDTO>(jsonObject.ToString());
                await this.productsExpensesService.UpdateProductsExpenseAsync(productsExpensesDTO);

                return Ok(productsExpensesDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.productsExpensesService.GetProductsExpenseByIdAsync(id).Result != null)
            {
                await this.productsExpensesService.DeleteProductsExpenseAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
