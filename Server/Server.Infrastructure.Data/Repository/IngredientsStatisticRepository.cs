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
    public class IngredientsStatisticRepository : BaseRepository<int, IngredientStatistic>
    {
        public IngredientsStatisticRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<IngredientStatistic> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async override Task UpdateAsync(IngredientStatistic value)
        {
            var statistic = await GetAsync(value.Id);
            statistic.IngredientId = value.IngredientId;
            statistic.CaloricInfoId = value.CaloricInfoId;

            ctx.Entry(statistic).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
