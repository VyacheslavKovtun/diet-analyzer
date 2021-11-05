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
    public class FavouriteRecipesRepository : BaseRepository<int, FavouriteRecipe>
    {
        public FavouriteRecipesRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<FavouriteRecipe> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async override Task UpdateAsync(FavouriteRecipe value)
        {
            var recipe = await GetAsync(value.Id);
            recipe.UserId = value.UserId;
            recipe.RecipeId = value.RecipeId;

            ctx.Entry(recipe).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
