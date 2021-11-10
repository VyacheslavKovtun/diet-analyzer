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
    public class RecipesBaseInfoService : IRecipesBaseInfoService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public RecipesBaseInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewRecipeBaseInfoAsync(RecipeBaseInfoDTO recipeBaseInfo)
        {
            var info = mapper.Mapper.Map<RecipeBaseInfo>(recipeBaseInfo);

            await unitOfWork.RecipesBaseInfoRepository.CreateAsync(info);
        }

        public async Task DeleteRecipeBaseInfoAsync(int id)
        {
            await unitOfWork.RecipesBaseInfoRepository.DeleteAsync(id);
        }

        public async Task<List<RecipeBaseInfoDTO>> GetAllRecipesBaseInfoAsync()
        {
            var info = await unitOfWork.RecipesBaseInfoRepository.GetAllAsync();

            return mapper.Mapper.Map<List<RecipeBaseInfoDTO>>(info);
        }

        public async Task<RecipeBaseInfoDTO> GetRecipeBaseInfoByIdAsync(int id)
        {
            var info = await unitOfWork.RecipesBaseInfoRepository.GetAsync(id);

            return mapper.Mapper.Map<RecipeBaseInfoDTO>(info);
        }

        public async Task UpdateRecipeBaseInfoAsync(RecipeBaseInfoDTO recipeBaseInfo)
        {
            var info = mapper.Mapper.Map<RecipeBaseInfo>(recipeBaseInfo);

            await unitOfWork.RecipesBaseInfoRepository.UpdateAsync(info);
        }
    }
}
