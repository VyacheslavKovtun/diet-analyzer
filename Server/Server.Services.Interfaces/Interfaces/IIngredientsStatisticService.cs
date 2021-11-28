using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IIngredientsStatisticService
    {
        Task CreateNewIngredientStatisticAsync(IngredientStatisticDTO ingredientStatistic);
        Task<List<IngredientStatisticDTO>> GetAllIngredientsStatisticAsync();
        Task<IngredientStatisticDTO> GetIngredientStatisticByIdAsync(int id);
        Task UpdateIngredientStatisticAsync(IngredientStatisticDTO ingredientStatistic);
        Task DeleteIngredientStatisticAsync(int id);
    }
}
