using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class FavouriteRecipe: BaseEntity<int>
    {
        public int RecipeId { get; set; }
        public Guid UserId { get; set; }
    }
}
