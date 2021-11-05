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
    public class ProductsExpensesRepository : BaseRepository<int, ProductsExpense>
    {
        public ProductsExpensesRepository(ApplicationDbContext ctx) : base(ctx) { }

        public async override Task<ProductsExpense> GetAsync(int id)
        {
            return await table.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async override Task UpdateAsync(ProductsExpense value)
        {
            var expense = await GetAsync(value.Id);
            expense.ProductId = value.ProductId;
            expense.UserId = value.UserId;
            expense.BaseInfoId = value.BaseInfoId;
            expense.PurchasingDate = value.PurchasingDate;
            expense.IsExpired = value.IsExpired;

            ctx.Entry(expense).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }
    }
}
