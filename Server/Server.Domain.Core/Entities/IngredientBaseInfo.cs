using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class IngredientBaseInfo: BaseEntity<int>
    {
        public int ApiId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
