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
    public class RecipesBaseInfoRepository : BaseRepository<int, RecipeBaseInfo>
    {
        public RecipesBaseInfoRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async Task<RecipeBaseInfo> GetByApiIdAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.ApiId == id);
        }

        public async override Task<RecipeBaseInfo> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task UpdateAsync(RecipeBaseInfo value)
        {
            var info = await GetAsync(value.Id);
            info.ApiId = value.ApiId;
            info.Title = value.Title;
            info.ImageUrl = value.ImageUrl;
            info.ImageType = value.ImageType;
            info.Calories = value.Calories;

            ctx.Entry(info).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
