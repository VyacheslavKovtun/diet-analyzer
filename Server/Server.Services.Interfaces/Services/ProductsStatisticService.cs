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
    public class ProductsStatisticService : IProductsStatisticService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ProductsStatisticService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewProductStatisticAsync(ProductStatisticDTO productStatistic)
        {
            var stat = mapper.Mapper.Map<ProductStatistic>(productStatistic);

            await unitOfWork.ProductsStatisticRepository.CreateAsync(stat);
        }

        public async Task DeleteProductStatisticAsync(int id)
        {
            await unitOfWork.ProductsStatisticRepository.DeleteAsync(id);
        }

        public async Task<List<ProductStatisticDTO>> GetAllProductsStatisticAsync()
        {
            var stat = await unitOfWork.ProductsStatisticRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ProductStatisticDTO>>(stat);
        }

        public async Task<ProductStatisticDTO> GetProductStatisticByIdAsync(int id)
        {
            var stat = await unitOfWork.ProductsStatisticRepository.GetAsync(id);

            return mapper.Mapper.Map<ProductStatisticDTO>(stat);
        }

        public async Task<ProductStatisticDTO> GetProductStatisticByProductBaseInfoIdAsync(int id)
        {
            var stat = await unitOfWork.ProductsStatisticRepository.GetByBaseInfoIdAsync(id);

            return mapper.Mapper.Map<ProductStatisticDTO>(stat);
        }

        public async Task UpdateProductStatisticAsync(ProductStatisticDTO productStatistic)
        {
            var stat = mapper.Mapper.Map<ProductStatistic>(productStatistic);

            await unitOfWork.ProductsStatisticRepository.UpdateAsync(stat);
        }
    }
}
