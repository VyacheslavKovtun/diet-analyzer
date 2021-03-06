using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IFavouriteRecipesService
    {
        Task CreateNewFavouriteRecipeAsync(FavouriteRecipeDTO favouriteRecipe);
        Task<List<FavouriteRecipeDTO>> GetAllFavouriteRecipesAsync();
        Task<FavouriteRecipeDTO> GetFavouriteRecipeByIdAsync(int id);
        Task<List<FavouriteRecipeDTO>> GetFavouriteRecipesByUserIdAsync(Guid id);
        Task<FavouriteRecipeDTO> GetFavouriteRecipeByRecipeBaseInfoIdAsync(int recipeId, Guid userId);
        Task UpdateFavouriteRecipeAsync(FavouriteRecipeDTO favouriteRecipe);
        Task DeleteFavouriteRecipeAsync(int id);
    }
}
