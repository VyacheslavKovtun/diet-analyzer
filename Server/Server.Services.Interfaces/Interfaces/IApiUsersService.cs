using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces.Interfaces
{
    public interface IApiUsersService
    {
        Task CreateNewApiUserAsync(ApiUserDTO apiUser);
        Task<List<ApiUserDTO>> GetAllApiUsersAsync();
        Task<ApiUserDTO> GetApiUserByIdAsync(Guid id);
        Task UpdateApiUserAsync(ApiUserDTO apiUser);
        Task DeleteApiUserAsync(Guid id);
    }
}
