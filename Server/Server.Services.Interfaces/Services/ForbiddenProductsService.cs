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
    public class ForbiddenProductsService : IForbiddenProductsService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ForbiddenProductsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewForbiddenProductAsync(ForbiddenProductDTO forbiddenProduct)
        {
            var prod = mapper.Mapper.Map<ForbiddenProduct>(forbiddenProduct);

            await unitOfWork.ForbiddenProductsRepository.CreateAsync(prod);
        }

        public async Task DeleteForbiddenProductAsync(int id)
        {
            await unitOfWork.ForbiddenProductsRepository.DeleteAsync(id);
        }

        public async Task<List<ForbiddenProductDTO>> GetAllForbiddenProductsAsync()
        {
            var prods = await unitOfWork.ForbiddenProductsRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ForbiddenProductDTO>>(prods);
        }

        public async Task<ForbiddenProductDTO> GetForbiddenProductByIdAsync(int id)
        {
            var prod = await unitOfWork.ForbiddenProductsRepository.GetAsync(id);

            return mapper.Mapper.Map<ForbiddenProductDTO>(prod);
        }

        public async Task UpdateForbiddenProductAsync(ForbiddenProductDTO forbiddenProduct)
        {
            var prod = mapper.Mapper.Map<ForbiddenProduct>(forbiddenProduct);

            await unitOfWork.ForbiddenProductsRepository.UpdateAsync(prod);
        }
    }
}
