using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IForbiddenIngredientsService
    {
        Task CreateNewForbiddenIngredientAsync(ForbiddenIngredientDTO forbiddenIngredient);
        Task<List<ForbiddenIngredientDTO>> GetAllForbiddenIngredientsAsync();
        Task<ForbiddenIngredientDTO> GetForbiddenIngredientByIdAsync(int id);
        Task<List<ForbiddenIngredientDTO>> GetForbiddenIngredientsByUserIdAsync(Guid id);
        Task<ForbiddenIngredientDTO> GetForbiddenIngredientByIngrBaseInfoIdAsync(int ingrId, Guid userId);
        Task UpdateForbiddenIngredientAsync(ForbiddenIngredientDTO forbiddenIngredient);
        Task DeleteForbiddenIngredientAsync(int id);
    }
}
