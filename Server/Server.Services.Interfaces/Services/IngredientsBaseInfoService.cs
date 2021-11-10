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
    public class IngredientsBaseInfoService : IIngredientsBaseInfoService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public IngredientsBaseInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewIngredientBaseInfoAsync(IngredientBaseInfoDTO ingredientBaseInfo)
        {
            var info = mapper.Mapper.Map<IngredientBaseInfo>(ingredientBaseInfo);

            await unitOfWork.IngredientsBaseInfoRepository.CreateAsync(info);
        }

        public async Task DeleteIngredientBaseInfoAsync(int id)
        {
            await unitOfWork.IngredientsBaseInfoRepository.DeleteAsync(id);
        }

        public async Task<List<IngredientBaseInfoDTO>> GetAllIngredientsBaseInfoAsync()
        {
            var info = await unitOfWork.IngredientsBaseInfoRepository.GetAllAsync();

            return mapper.Mapper.Map<List<IngredientBaseInfoDTO>>(info);
        }

        public async Task<IngredientBaseInfoDTO> GetIngredientBaseInfoByIdAsync(int id)
        {
            var info = await unitOfWork.IngredientsBaseInfoRepository.GetAsync(id);

            return mapper.Mapper.Map<IngredientBaseInfoDTO>(info);
        }

        public async Task UpdateIngredientBaseInfoAsync(IngredientBaseInfoDTO ingredientBaseInfo)
        {
            var info = mapper.Mapper.Map<IngredientBaseInfo>(ingredientBaseInfo);

            await unitOfWork.IngredientsBaseInfoRepository.UpdateAsync(info);
        }
    }
}
