using Server.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ApiUsersRepository ApiUsersRepository { get; }
        BaseInfoRepository BaseInfoRepository { get; }
        CaloricInfoRepository CaloricInfoRepository { get; }

        IngredientsBaseInfoRepository IngredientsBaseInfoRepository { get; }
        CurrentIngredientsRepository CurrentIngredientsRepository { get; }
        ForbiddenIngredientsRepository ForbiddenIngredientsRepository { get; }
        IngredientsStatisticRepository IngredientsStatisticRepository { get; }
        IngredientsExpensesRepository IngredientsExpensesRepository { get; }

        ProductsBaseInfoRepository ProductsBaseInfoRepository { get; }
        CurrentProductsRepository CurrentProductsRepository { get; }
        ForbiddenProductsRepository ForbiddenProductsRepository { get; }
        ProductsStatisticRepository ProductsStatisticRepository { get; }
        ProductsExpensesRepository ProductsExpensesRepository { get; }

        RecipesBaseInfoRepository RecipesBaseInfoRepository { get; }
        FavouriteRecipesRepository FavouriteRecipesRepository { get; }
    }
}
