using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface ICurrentProductsService
    {
        Task CreateNewCurrentProductAsync(CurrentProductDTO currentProduct);
        Task<List<CurrentProductDTO>> GetAllCurrentProductsAsync();
        Task<CurrentProductDTO> GetCurrentProductByIdAsync(int id);
        Task<List<CurrentProductDTO>> GetCurrentProductsByUserIdAsync(Guid id);
        Task<CurrentProductDTO> GetCurrentProductByProductBaseInfoIdAsync(int infoId, Guid userId);
        Task UpdateCurrentProductAsync(CurrentProductDTO currentProduct);
        Task DeleteCurrentProductAsync(int id);
    }
}
