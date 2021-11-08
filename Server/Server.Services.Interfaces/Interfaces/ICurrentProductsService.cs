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
        Task UpdateCurrentProductAsync(CurrentProductDTO currentProduct);
        Task DeleteCurrentProductAsync(int id);
    }
}
