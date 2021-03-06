using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IProductsExpensesService
    {
        Task CreateNewProductsExpenseAsync(ProductsExpenseDTO productsExpense);
        Task<List<ProductsExpenseDTO>> GetAllProductsExpensesAsync();
        Task<ProductsExpenseDTO> GetProductsExpenseByIdAsync(int id);
        Task<List<ProductsExpenseDTO>> GetProductsExpensesByUserIdAsync(Guid id);
        Task<ProductsExpenseDTO> GetProductsExpenseByProductBaseInfoIdAsync(int infoId, Guid userId);
        Task UpdateProductsExpenseAsync(ProductsExpenseDTO productsExpense);
        Task DeleteProductsExpenseAsync(int id);
    }
}
