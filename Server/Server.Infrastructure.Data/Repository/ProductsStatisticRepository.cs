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
    public class ProductsStatisticRepository : BaseRepository<int, ProductStatistic>
    {
        public ProductsStatisticRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<ProductStatistic> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async override Task UpdateAsync(ProductStatistic value)
        {
            var stat = await GetAsync(value.Id);
            stat.ProductId = value.ProductId;
            stat.CaloricInfoId = value.CaloricInfoId;

            ctx.Entry(stat).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
