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
    public class CurrentIngredientsRepository : BaseRepository<int, CurrentIngredient>
    {
        public CurrentIngredientsRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<CurrentIngredient> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task UpdateAsync(CurrentIngredient value)
        {
            var ingredient = await GetAsync(value.Id);
            ingredient.IngredientId = value.IngredientId;
            ingredient.UserId = value.UserId;
            ingredient.BaseInfoId = value.BaseInfoId;

            ctx.Entry(ingredient).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
