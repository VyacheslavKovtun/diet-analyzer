using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Domain.Core.Entities;

namespace Server.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApiUser> ApiUsers { get; set; }

        public DbSet<BaseInfo> BaseInfo { get; set; }
        public DbSet<CaloricInfo> CaloricInfo { get; set; }

        public DbSet<ProductBaseInfo> ProductsBaseInfo { get; set; }
        public DbSet<CurrentProduct> CurrentProducts { get; set; }
        public DbSet<ForbiddenProduct> ForbiddenProducts { get; set; }
        public DbSet<ProductStatistic> ProductsStatistic { get; set; }
        public DbSet<ProductsExpense> ProductsExpenses { get; set; }

        public DbSet<IngredientBaseInfo> IngredientsBaseInfo { get; set; }
        public DbSet<CurrentIngredient> CurrentIngredients { get; set; }
        public DbSet<ForbiddenIngredient> ForbiddenIngredients { get; set; }
        public DbSet<IngredientStatistic> IngredientsStatistic { get; set; }
        public DbSet<IngredientsExpense> IngredientsExpenses { get; set; }

        public DbSet<RecipeBaseInfo> RecipesBaseInfo { get; set; }
        public DbSet<FavouriteRecipe> FavouriteRecipes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
