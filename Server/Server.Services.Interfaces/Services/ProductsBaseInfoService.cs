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
    public class ProductsBaseInfoService : IProductsBaseInfoService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ProductsBaseInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewProductBaseInfoAsync(ProductBaseInfoDTO productBaseInfo)
        {
            var info = mapper.Mapper.Map<ProductBaseInfo>(productBaseInfo);

            await unitOfWork.ProductsBaseInfoRepository.CreateAsync(info);
        }

        public async Task DeleteProductBaseInfoAsync(int id)
        {
            await unitOfWork.ProductsBaseInfoRepository.DeleteAsync(id);
        }

        public async Task<List<ProductBaseInfoDTO>> GetAllProductsBaseInfoAsync()
        {
            var info = await unitOfWork.ProductsBaseInfoRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ProductBaseInfoDTO>>(info);
        }

        public async Task<ProductBaseInfoDTO> GetProductBaseInfoByIdAsync(int id)
        {
            var info = await unitOfWork.ProductsBaseInfoRepository.GetAsync(id);

            return mapper.Mapper.Map<ProductBaseInfoDTO>(info);
        }

        public async Task<ProductBaseInfoDTO> GetProductBaseInfoByApiIdAsync(int apiId)
        {
            var info = await unitOfWork.ProductsBaseInfoRepository.GetByApiIdAsync(apiId);

            return mapper.Mapper.Map<ProductBaseInfoDTO>(info);
        }

        public async Task UpdateProductBaseInfoAsync(ProductBaseInfoDTO productBaseInfo)
        {
            var info = mapper.Mapper.Map<ProductBaseInfo>(productBaseInfo);

            await unitOfWork.ProductsBaseInfoRepository.UpdateAsync(info);
        }
    }
}
