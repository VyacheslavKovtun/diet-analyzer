using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface ICaloricInfoService
    {
        Task CreateNewCaloricInfoAsync(CaloricInfoDTO caloricInfo);
        Task<List<CaloricInfoDTO>> GetAllCaloricInfoAsync();
        Task<CaloricInfoDTO> GetCaloricInfoByIdAsync(int id);
        Task<CaloricInfoDTO> GetCaloricInfoByFieldsAsync(CaloricInfoDTO caloricInfo);
        Task UpdateCaloricInfoAsync(CaloricInfoDTO caloricInfo);
        Task DeleteCaloricInfoAsync(int id);
    }
}
