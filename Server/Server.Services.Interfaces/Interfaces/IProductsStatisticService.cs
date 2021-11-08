using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IProductsStatisticService
    {
        Task CreateNewProductStatisticAsync(ProductStatisticDTO productStatistic);
        Task<List<ProductStatisticDTO>> GetAllProductsStatisticAsync();
        Task<ProductStatisticDTO> GetProductStatisticByIdAsync(int id);
        Task UpdateProductStatisticAsync(ProductStatisticDTO productStatistic);
        Task DeleteProductStatisticAsync(int id);
    }
}
