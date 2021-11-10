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
    public class CaloricInfoService : ICaloricInfoService
    {
        IUnitOfWork unitOfWork;
        AutoMap mapper = AutoMap.Instance;

        public CaloricInfoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateNewCaloricInfoAsync(CaloricInfoDTO caloricInfo)
        {
            var info = mapper.Mapper.Map<CaloricInfo>(caloricInfo);

            await unitOfWork.CaloricInfoRepository.CreateAsync(info);
        }

        public async Task DeleteCaloricInfoAsync(int id)
        {
            await unitOfWork.CaloricInfoRepository.DeleteAsync(id);
        }

        public async Task<List<CaloricInfoDTO>> GetAllCaloricInfoAsync()
        {
            var info = await unitOfWork.CaloricInfoRepository.GetAllAsync();

            return mapper.Mapper.Map<List<CaloricInfoDTO>>(info);
        }

        public async Task<CaloricInfoDTO> GetCaloricInfoByIdAsync(int id)
        {
            var info = await unitOfWork.CaloricInfoRepository.GetAsync(id);

            return mapper.Mapper.Map<CaloricInfoDTO>(info);
        }

        public async Task UpdateCaloricInfoAsync(CaloricInfoDTO caloricInfo)
        {
            var info = mapper.Mapper.Map<CaloricInfo>(caloricInfo);

            await unitOfWork.CaloricInfoRepository.UpdateAsync(info);
        }
    }
}
