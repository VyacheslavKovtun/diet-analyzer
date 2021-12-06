using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IProductsBaseInfoService
    {
        Task CreateNewProductBaseInfoAsync(ProductBaseInfoDTO productBaseInfo);
        Task<List<ProductBaseInfoDTO>> GetAllProductsBaseInfoAsync();
        Task<ProductBaseInfoDTO> GetProductBaseInfoByIdAsync(int id);
        Task<ProductBaseInfoDTO> GetProductBaseInfoByApiIdAsync(int apiId);
        Task UpdateProductBaseInfoAsync(ProductBaseInfoDTO productBaseInfo);
        Task DeleteProductBaseInfoAsync(int id);
    }
}
