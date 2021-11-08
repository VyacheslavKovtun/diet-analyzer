using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IRecipesBaseInfoService
    {
        Task CreateNewRecipeBaseInfoAsync(RecipeBaseInfoDTO recipeBaseInfo);
        Task<List<RecipeBaseInfoDTO>> GetAllRecipesBaseInfoAsync();
        Task<RecipeBaseInfoDTO> GetRecipeBaseInfoByIdAsync(int id);
        Task UpdateRecipeBaseInfoAsync(RecipeBaseInfoDTO recipeBaseInfo);
        Task DeleteRecipeBaseInfoAsync(int id);
    }
}
