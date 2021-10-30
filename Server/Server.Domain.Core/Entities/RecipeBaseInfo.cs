using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class RecipeBaseInfo: BaseEntity<int>
    {
        public int ApiId { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
    }
}
