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
    public class FavouriteRecipesService : IFavouriteRecipesService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public FavouriteRecipesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewFavouriteRecipeAsync(FavouriteRecipeDTO favouriteRecipe)
        {
            var recipe = mapper.Mapper.Map<FavouriteRecipe>(favouriteRecipe);

            await unitOfWork.FavouriteRecipesRepository.CreateAsync(recipe);
        }

        public async Task DeleteFavouriteRecipeAsync(int id)
        {
            await unitOfWork.FavouriteRecipesRepository.DeleteAsync(id);
        }

        public async Task<List<FavouriteRecipeDTO>> GetAllFavouriteRecipesAsync()
        {
            var recipes = await unitOfWork.FavouriteRecipesRepository.GetAllAsync();

            return mapper.Mapper.Map<List<FavouriteRecipeDTO>>(recipes);
        }

        public async Task<FavouriteRecipeDTO> GetFavouriteRecipeByIdAsync(int id)
        {
            var recipe = await unitOfWork.FavouriteRecipesRepository.GetAsync(id);

            return mapper.Mapper.Map<FavouriteRecipeDTO>(recipe);
        }

        public async Task<List<FavouriteRecipeDTO>> GetFavouriteRecipesByUserIdAsync(Guid id)
        {
            var recipes = await GetAllFavouriteRecipesAsync();

            return recipes.FindAll(r => r.UserId == id);
        }

        public async Task UpdateFavouriteRecipeAsync(FavouriteRecipeDTO favouriteRecipe)
        {
            var recipe = mapper.Mapper.Map<FavouriteRecipe>(favouriteRecipe);

            await unitOfWork.FavouriteRecipesRepository.UpdateAsync(recipe);
        }
    }
}
