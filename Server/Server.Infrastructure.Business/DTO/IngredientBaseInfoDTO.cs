using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class IngredientBaseInfoDTO
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
