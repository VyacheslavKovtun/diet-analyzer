using Server.Domain.Core.Entities;
using Server.Infrastructure.Business.AutoMapper;
using Server.Infrastructure.Business.DTO;
using Server.Infrastructure.Data.UnitOfWork;
using Server.Services.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Services
{
    public class ProductsExpensesService : IProductsExpensesService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ProductsExpensesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewProductsExpenseAsync(ProductsExpenseDTO productsExpense)
        {
            var exp = mapper.Mapper.Map<ProductsExpense>(productsExpense);

            await unitOfWork.ProductsExpensesRepository.CreateAsync(exp);
        }

        public async Task DeleteProductsExpenseAsync(int id)
        {
            await unitOfWork.ProductsExpensesRepository.DeleteAsync(id);
        }

        public async Task<List<ProductsExpenseDTO>> GetAllProductsExpensesAsync()
        {
            var exp = await unitOfWork.ProductsExpensesRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ProductsExpenseDTO>>(exp);
        }

        public async Task<ProductsExpenseDTO> GetProductsExpenseByIdAsync(int id)
        {
            var exp = await unitOfWork.ProductsExpensesRepository.GetAsync(id);

            return mapper.Mapper.Map<ProductsExpenseDTO>(exp);
        }

        public async Task UpdateProductsExpenseAsync(ProductsExpenseDTO productsExpense)
        {
            var exp = mapper.Mapper.Map<ProductsExpense>(productsExpense);

            await unitOfWork.ProductsExpensesRepository.UpdateAsync(exp);
        }
    }
}
