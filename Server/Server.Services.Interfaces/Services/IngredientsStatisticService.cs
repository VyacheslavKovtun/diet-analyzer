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
    public class IngredientsStatisticService : IIngredientsStatisticService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public IngredientsStatisticService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewIngredientStatisticAsync(IngredientStatisticDTO ingredientStatistic)
        {
            var stat = mapper.Mapper.Map<IngredientStatistic>(ingredientStatistic);

            await unitOfWork.IngredientsStatisticRepository.CreateAsync(stat);
        }

        public async Task DeleteIngredientStatisticAsync(int id)
        {
            await unitOfWork.IngredientsStatisticRepository.DeleteAsync(id);
        }

        public async Task<List<IngredientStatisticDTO>> GetAllIngredientsStatisticAsync()
        {
            var stat = await unitOfWork.IngredientsStatisticRepository.GetAllAsync();

            return mapper.Mapper.Map<List<IngredientStatisticDTO>>(stat);
        }

        public async Task<IngredientStatisticDTO> GetIngredientStatisticByIdAsync(int id)
        {
            var stat = await unitOfWork.IngredientsStatisticRepository.GetAsync(id);

            return mapper.Mapper.Map<IngredientStatisticDTO>(stat);
        }

        public async Task UpdateIngredientsStatisticAsync(IngredientStatisticDTO ingredientStatistic)
        {
            var stat = mapper.Mapper.Map<IngredientStatistic>(ingredientStatistic);

            await unitOfWork.IngredientsStatisticRepository.UpdateAsync(stat);
        }
    }
}
