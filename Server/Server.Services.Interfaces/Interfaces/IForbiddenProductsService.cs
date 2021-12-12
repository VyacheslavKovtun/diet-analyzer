using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IForbiddenProductsService
    {
        Task CreateNewForbiddenProductAsync(ForbiddenProductDTO forbiddenProduct);
        Task<List<ForbiddenProductDTO>> GetAllForbiddenProductsAsync();
        Task<ForbiddenProductDTO> GetForbiddenProductByIdAsync(int id);
        Task<List<ForbiddenProductDTO>> GetForbiddenProductsByUserIdAsync(Guid id);
        Task<ForbiddenProductDTO> GetForbiddenProductByProdBaseInfoIdAsync(int prodId, Guid userId);
        Task UpdateForbiddenProductAsync(ForbiddenProductDTO forbiddenProduct);
        Task DeleteForbiddenProductAsync(int id);
    }
}
