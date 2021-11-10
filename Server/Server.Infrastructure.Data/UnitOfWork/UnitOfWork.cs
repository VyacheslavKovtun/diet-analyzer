using Server.Data;
using Server.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext ctx;

        ApiUsersRepository _apiUsersRepository;
        BaseInfoRepository _baseInfoRepository;
        CaloricInfoRepository _caloricInfoRepository;
        IngredientsBaseInfoRepository _ingredientsBaseInfoRepository;
        CurrentIngredientsRepository _currentIngredientsRepository;
        ForbiddenIngredientsRepository _forbiddenIngredientsRepository;
        IngredientsStatisticRepository _ingredientsStatisticRepository;
        IngredientsExpensesRepository _ingredientsExpensesRepository;
        ProductsBaseInfoRepository _productsBaseInfoRepository;
        CurrentProductsRepository _currentProductsRepository;
        ForbiddenProductsRepository _forbiddenProductsRepository;
        ProductsStatisticRepository _productsStatisticRepository;
        ProductsExpensesRepository _productsExpensesRepository;
        RecipesBaseInfoRepository _recipesBaseInfoRepository;
        FavouriteRecipesRepository _favouriteRecipesRepository;

        public ApiUsersRepository ApiUsersRepository => _apiUsersRepository 
            ?? (_apiUsersRepository = new ApiUsersRepository(ctx));

        public BaseInfoRepository BaseInfoRepository => _baseInfoRepository
            ?? (_baseInfoRepository = new BaseInfoRepository(ctx));

        public CaloricInfoRepository CaloricInfoRepository => _caloricInfoRepository
            ?? (_caloricInfoRepository = new CaloricInfoRepository(ctx));

        public IngredientsBaseInfoRepository IngredientsBaseInfoRepository => _ingredientsBaseInfoRepository
            ?? (_ingredientsBaseInfoRepository = new IngredientsBaseInfoRepository(ctx));

        public CurrentIngredientsRepository CurrentIngredientsRepository => _currentIngredientsRepository
            ?? (_currentIngredientsRepository = new CurrentIngredientsRepository(ctx));

        public ForbiddenIngredientsRepository ForbiddenIngredientsRepository => _forbiddenIngredientsRepository
            ?? (_forbiddenIngredientsRepository = new ForbiddenIngredientsRepository(ctx));

        public IngredientsStatisticRepository IngredientsStatisticRepository => _ingredientsStatisticRepository
            ?? (_ingredientsStatisticRepository = new IngredientsStatisticRepository(ctx));

        public IngredientsExpensesRepository IngredientsExpensesRepository => _ingredientsExpensesRepository
            ?? (_ingredientsExpensesRepository = new IngredientsExpensesRepository(ctx));

        public ProductsBaseInfoRepository ProductsBaseInfoRepository => _productsBaseInfoRepository
            ?? (_productsBaseInfoRepository = new ProductsBaseInfoRepository(ctx));

        public CurrentProductsRepository CurrentProductsRepository => _currentProductsRepository
            ?? (_currentProductsRepository = new CurrentProductsRepository(ctx));

        public ForbiddenProductsRepository ForbiddenProductsRepository => _forbiddenProductsRepository
            ?? (_forbiddenProductsRepository = new ForbiddenProductsRepository(ctx));

        public ProductsStatisticRepository ProductsStatisticRepository => _productsStatisticRepository
            ?? (_productsStatisticRepository = new ProductsStatisticRepository(ctx));

        public ProductsExpensesRepository ProductsExpensesRepository => _productsExpensesRepository
            ?? (_productsExpensesRepository = new ProductsExpensesRepository(ctx));

        public RecipesBaseInfoRepository RecipesBaseInfoRepository => _recipesBaseInfoRepository
            ?? (_recipesBaseInfoRepository = new RecipesBaseInfoRepository(ctx));

        public FavouriteRecipesRepository FavouriteRecipesRepository => _favouriteRecipesRepository
            ?? (_favouriteRecipesRepository = new FavouriteRecipesRepository(ctx));

        public UnitOfWork(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if (disposing)
                    ctx.Dispose();

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
