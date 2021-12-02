using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IBaseInfoService
    {
        Task CreateNewBaseInfoAsync(BaseInfoDTO baseInfo);
        Task<List<BaseInfoDTO>> GetAllBaseInfoAsync();
        Task<BaseInfoDTO> GetBaseInfoByIdAsync(int id);
        Task<BaseInfoDTO> GetByFieldsAsync(BaseInfoDTO baseInfo);
        Task UpdateBaseInfoAsync(BaseInfoDTO baseInfo);
        Task DeleteBaseInfoAsync(int id);
    }
}
