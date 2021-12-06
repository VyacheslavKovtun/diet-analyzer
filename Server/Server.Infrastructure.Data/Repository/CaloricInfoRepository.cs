using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Domain.Core.Entities;
using Server.Infrastructure.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Data.Repository
{
    public class CaloricInfoRepository : BaseRepository<int, CaloricInfo>
    {
        public CaloricInfoRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<CaloricInfo> GetByFieldsAsync(CaloricInfo info)
        {
            return await table.FirstOrDefaultAsync(i => i.Calories == info.Calories &&
            i.Protein == info.Protein && i.Fat == info.Fat);
        }

        public async override Task<CaloricInfo> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async override Task UpdateAsync(CaloricInfo value)
        {
            var info = await GetAsync(value.Id);
            info.Calories = value.Calories;
            info.Fat = value.Fat;
            info.Protein = value.Protein;

            ctx.Entry(info).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
