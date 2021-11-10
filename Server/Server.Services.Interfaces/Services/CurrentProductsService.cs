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
    public class CurrentProductsService : ICurrentProductsService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public CurrentProductsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewCurrentProductAsync(CurrentProductDTO currentProduct)
        {
            var prod = mapper.Mapper.Map<CurrentProduct>(currentProduct);

            await unitOfWork.CurrentProductsRepository.CreateAsync(prod);
        }

        public async Task DeleteCurrentProductAsync(int id)
        {
            await unitOfWork.CurrentProductsRepository.DeleteAsync(id);
        }

        public async Task<List<CurrentProductDTO>> GetAllCurrentProductsAsync()
        {
            var prods = await unitOfWork.CurrentProductsRepository.GetAllAsync();

            return mapper.Mapper.Map<List<CurrentProductDTO>>(prods);
        }

        public async Task<CurrentProductDTO> GetCurrentProductByIdAsync(int id)
        {
            var prod = await unitOfWork.CurrentProductsRepository.GetAsync(id);

            return mapper.Mapper.Map<CurrentProductDTO>(prod);
        }

        public async Task<List<CurrentProductDTO>> GetCurrentProductsByUserIdAsync(Guid id)
        {
            var prods = await GetAllCurrentProductsAsync();

            return prods.FindAll(p => p.UserId == id);
        }

        public async Task UpdateCurrentProductAsync(CurrentProductDTO currentProduct)
        {
            var prod = mapper.Mapper.Map<CurrentProduct>(currentProduct);

            await unitOfWork.CurrentProductsRepository.UpdateAsync(prod);
        }
    }
}
