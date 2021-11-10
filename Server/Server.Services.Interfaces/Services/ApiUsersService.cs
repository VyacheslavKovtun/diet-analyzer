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
    public class ApiUsersService : IApiUsersService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public ApiUsersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewApiUserAsync(ApiUserDTO apiUser)
        {
            var user = mapper.Mapper.Map<ApiUser>(apiUser);

            await unitOfWork.ApiUsersRepository.CreateAsync(user);
        }

        public async Task DeleteApiUserAsync(Guid id)
        {
            await unitOfWork.ApiUsersRepository.DeleteAsync(id);
        }

        public async Task<List<ApiUserDTO>> GetAllApiUsersAsync()
        {
            var users = await unitOfWork.ApiUsersRepository.GetAllAsync();

            return mapper.Mapper.Map<List<ApiUserDTO>>(users);
        }

        public async Task<ApiUserDTO> GetApiUserByIdAsync(Guid id)
        {
            var user = await unitOfWork.ApiUsersRepository.GetAsync(id);

            return mapper.Mapper.Map<ApiUserDTO>(user);
        }

        public async Task UpdateApiUserAsync(ApiUserDTO apiUser)
        {
            var user = mapper.Mapper.Map<ApiUser>(apiUser);

            await unitOfWork.ApiUsersRepository.UpdateAsync(user);
        }
    }
}
