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
    public class BaseInfoRepository : BaseRepository<int, BaseInfo>
    {
        public BaseInfoRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<BaseInfo> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task UpdateAsync(BaseInfo value)
        {
            var info = await GetAsync(value.Id);
            info.Price = value.Price;
            info.Amount = value.Amount;
            info.Unit = value.Unit;
            info.ExpirationDate = value.ExpirationDate;
            info.MealType = value.MealType;

            ctx.Entry(info).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
