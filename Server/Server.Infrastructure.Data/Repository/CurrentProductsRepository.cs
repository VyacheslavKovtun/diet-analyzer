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
    public class CurrentProductsRepository : BaseRepository<int, CurrentProduct>
    {
        public CurrentProductsRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<CurrentProduct> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async override Task UpdateAsync(CurrentProduct value)
        {
            var prod = await GetAsync(value.Id);
            prod.ProductId = value.ProductId;
            prod.UserId = value.UserId;
            prod.BaseInfoId = value.BaseInfoId;

            ctx.Entry(prod).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
