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
    public class ForbiddenIngredientsService : IForbiddenIngredientsService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ForbiddenIngredientsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewForbiddenIngredientAsync(ForbiddenIngredientDTO forbiddenIngredient)
        {
            var ingr = mapper.Mapper.Map<ForbiddenIngredient>(forbiddenIngredient);

            await unitOfWork.ForbiddenIngredientsRepository.CreateAsync(ingr);
        }

        public async Task DeleteForbiddenIngredientAsync(int id)
        {
            await unitOfWork.ForbiddenIngredientsRepository.DeleteAsync(id);
        }

        public async Task<List<ForbiddenIngredientDTO>> GetAllForbiddenIngredientsAsync()
        {
            var ingrs = await unitOfWork.ForbiddenIngredientsRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ForbiddenIngredientDTO>>(ingrs);
        }

        public async Task<ForbiddenIngredientDTO> GetForbiddenIngredientByIdAsync(int id)
        {
            var ingr = await unitOfWork.ForbiddenIngredientsRepository.GetAsync(id);

            return mapper.Mapper.Map<ForbiddenIngredientDTO>(ingr);
        }

        public async Task UpdateForbiddenIngredientAsync(ForbiddenIngredientDTO forbiddenIngredient)
        {
            var ingr = mapper.Mapper.Map<ForbiddenIngredient>(forbiddenIngredient);

            await unitOfWork.ForbiddenIngredientsRepository.UpdateAsync(ingr);
        }
    }
}
