using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class FavouriteRecipeDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}
