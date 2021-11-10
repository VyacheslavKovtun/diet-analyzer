using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IIngredientsBaseInfoService
    {
        Task CreateNewIngredientBaseInfoAsync(IngredientBaseInfoDTO ingredientBaseInfo);
        Task<List<IngredientBaseInfoDTO>> GetAllIngredientsBaseInfoAsync();
        Task<IngredientBaseInfoDTO> GetIngredientBaseInfoByIdAsync(int id);
        Task UpdateIngredientBaseInfoAsync(IngredientBaseInfoDTO ingredientBaseInfo);
        Task DeleteIngredientBaseInfoAsync(int id);
    }
}
