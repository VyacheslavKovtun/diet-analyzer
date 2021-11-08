using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class RecipeBaseInfoDTO
    {
        public int Id { get; set; }
        public int ApiId { get; set; }
        public string Title { get; set; }
        public int Calories { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
    }
}
