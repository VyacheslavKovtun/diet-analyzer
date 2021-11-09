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
    public class BaseInfoService : IBaseInfoService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public BaseInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewBaseInfoAsync(BaseInfoDTO baseInfo)
        {
            var info = mapper.Mapper.Map<BaseInfo>(baseInfo);

            await unitOfWork.BaseInfoRepository.CreateAsync(info);
        }

        public async Task DeleteBaseInfoAsync(int id)
        {
            await unitOfWork.BaseInfoRepository.DeleteAsync(id);
        }

        public async Task<List<BaseInfoDTO>> GetAllBaseInfoAsync()
        {
            var info = await unitOfWork.BaseInfoRepository.GetAllAsync();

            return mapper.Mapper.Map<List<BaseInfoDTO>>(info);
        }

        public async Task<BaseInfoDTO> GetBaseInfoByIdAsync(int id)
        {
            var info = await unitOfWork.BaseInfoRepository.GetAsync(id);

            return mapper.Mapper.Map<BaseInfoDTO>(info);
        }

        public async Task UpdateBaseInfoAsync(BaseInfoDTO baseInfo)
        {
            var info = mapper.Mapper.Map<BaseInfo>(baseInfo);

            await unitOfWork.BaseInfoRepository.UpdateAsync(info);
        }
    }
}
