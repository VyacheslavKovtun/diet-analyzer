using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface ICurrentIngredientsService
    {
        Task CreateNewCurrentIngredientAsync(CurrentIngredientDTO currentIngredient);
        Task<List<CurrentIngredientDTO>> GetAllCurrentIngredientsAsync();
        Task<CurrentIngredientDTO> GetCurrentIngredientByIdAsync(int id);
        Task<List<CurrentIngredientDTO>> GetCurrentIngredientsByUserIdAsync(Guid id);
        Task<CurrentIngredientDTO> GetCurrentIngredientByIngredientBaseInfoIdAsync(int ingrId, Guid userId);
        Task UpdateCurrentIngredientAsync(CurrentIngredientDTO currentIngredient);
        Task DeleteCurrentIngredientAsync(int id);
    }
}
