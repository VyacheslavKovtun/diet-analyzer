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
    public class IngredientsBaseInfoRepository : BaseRepository<int, IngredientBaseInfo>
    {
        public IngredientsBaseInfoRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<IngredientBaseInfo> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async override Task UpdateAsync(IngredientBaseInfo value)
        {
            var info = await GetAsync(value.Id);
            info.ApiId = value.ApiId;
            info.Name = value.Name;
            info.ImageUrl = value.ImageUrl;

            ctx.Entry(info).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
