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
    public class ForbiddenIngredientsRepository : BaseRepository<int, ForbiddenIngredient>
    {
        public ForbiddenIngredientsRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<ForbiddenIngredient> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task UpdateAsync(ForbiddenIngredient value)
        {
            var ingredient = await GetAsync(value.Id);
            ingredient.IngredientId = value.IngredientId;
            ingredient.UserId = value.UserId;

            ctx.Entry(ingredient).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
