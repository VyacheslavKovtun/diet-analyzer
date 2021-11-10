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
    public class CurrentIngredientsService: ICurrentIngredientsService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public CurrentIngredientsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewCurrentIngredientAsync(CurrentIngredientDTO currentIngredient)
        {
            var ingr = mapper.Mapper.Map<CurrentIngredient>(currentIngredient);

            await unitOfWork.CurrentIngredientsRepository.CreateAsync(ingr);
        }

        public async Task DeleteCurrentIngredientAsync(int id)
        {
            await unitOfWork.CurrentIngredientsRepository.DeleteAsync(id);
        }

        public async Task<List<CurrentIngredientDTO>> GetAllCurrentIngredientsAsync()
        {
            var ingrs = await unitOfWork.CurrentIngredientsRepository.GetAllAsync();

            return mapper.Mapper.Map<List<CurrentIngredientDTO>>(ingrs);
        }

        public async Task<CurrentIngredientDTO> GetCurrentIngredientByIdAsync(int id)
        {
            var ingr = await unitOfWork.CurrentIngredientsRepository.GetAsync(id);

            return mapper.Mapper.Map<CurrentIngredientDTO>(ingr);
        }

        public async Task<List<CurrentIngredientDTO>> GetCurrentIngredientsByUserIdAsync(Guid id)
        {
            var ingrs = await GetAllCurrentIngredientsAsync();

            return ingrs.FindAll(i => i.UserId == id);
        }

        public async Task UpdateCurrentIngredientAsync(CurrentIngredientDTO currentIngredient)
        {
            var ingr = mapper.Mapper.Map<CurrentIngredient>(currentIngredient);

            await unitOfWork.CurrentIngredientsRepository.UpdateAsync(ingr);
        }
    }
}
