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
    public class ApiUsersRepository : BaseRepository<Guid, ApiUser>
    {
        public ApiUsersRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<ApiUser> GetAsync(Guid id)
        {
            return await table.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async override Task UpdateAsync(ApiUser value)
        {
            var user = await GetAsync(value.Id);
            user.Username = value.Username;
            user.ApiPassword = value.ApiPassword;
            user.Hash = value.Hash;

            ctx.Entry(user).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
