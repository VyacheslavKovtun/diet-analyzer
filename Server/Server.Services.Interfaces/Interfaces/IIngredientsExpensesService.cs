using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IIngredientsExpensesService
    {
        Task CreateNewIngredientsExpenseAsync(IngredientsExpenseDTO ingredientsExpense);
        Task<List<IngredientsExpenseDTO>> GetAllIngredientsExpensesAsync();
        Task<IngredientsExpenseDTO> GetIngredientsExpenseByIdAsync(int id);
        Task<List<IngredientsExpenseDTO>> GetIngredientsExpensesByUserIdAsync(Guid id);
        Task<IngredientsExpenseDTO> GetIngredientsExpenseByIngredientBaseInfoIdAsync(int infoId, Guid userId);
        Task UpdateIngredientsExpenseAsync(IngredientsExpenseDTO ingredientsExpense);
        Task DeleteIngredientsExpenseAsync(int id);
    }
}
