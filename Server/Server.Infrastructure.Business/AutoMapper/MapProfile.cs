using AutoMapper;
using Server.Domain.Core.Entities;
using Server.Infrastructure.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.AutoMapper
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<ApiUser, ApiUserDTO>();
            CreateMap<ApiUserDTO, ApiUser>();

            CreateMap<BaseInfo, BaseInfoDTO>();
            CreateMap<BaseInfoDTO, BaseInfo>();

            CreateMap<CaloricInfo, CaloricInfoDTO>();
            CreateMap<CaloricInfoDTO, CaloricInfo>();


            CreateMap<IngredientBaseInfo, IngredientBaseInfoDTO>();
            CreateMap<IngredientBaseInfoDTO, IngredientBaseInfo>();

            CreateMap<CurrentIngredient, CurrentIngredientDTO>();
            CreateMap<CurrentIngredientDTO, CurrentIngredient>();
            
            CreateMap<ForbiddenIngredient, ForbiddenIngredientDTO>();
            CreateMap<ForbiddenIngredientDTO, ForbiddenIngredient>();
            
            CreateMap<IngredientStatistic, IngredientStatisticDTO>();
            CreateMap<IngredientStatisticDTO, IngredientStatistic>();
            
            CreateMap<IngredientsExpense, IngredientsExpenseDTO>();
            CreateMap<IngredientsExpenseDTO, IngredientsExpense>();

            
            CreateMap<ProductBaseInfo, ProductBaseInfoDTO>();
            CreateMap<ProductBaseInfoDTO, ProductBaseInfo>();
            
            CreateMap<CurrentProduct, CurrentProductDTO>();
            CreateMap<CurrentProductDTO, CurrentProduct>();
            
            CreateMap<ForbiddenProduct, ForbiddenProductDTO>();
            CreateMap<ForbiddenProductDTO, ForbiddenProduct>();
            
            CreateMap<ProductStatistic, ProductStatisticDTO>();
            CreateMap<ProductStatisticDTO, ProductStatistic>();
            
            CreateMap<ProductsExpense, ProductsExpenseDTO>();
            CreateMap<ProductsExpenseDTO, ProductsExpense>();


            CreateMap<RecipeBaseInfo, RecipeBaseInfoDTO>();
            CreateMap<RecipeBaseInfoDTO, RecipeBaseInfo>();

            CreateMap<FavouriteRecipe, FavouriteRecipeDTO>();
            CreateMap<FavouriteRecipeDTO, FavouriteRecipe>();
        }
    }
}
