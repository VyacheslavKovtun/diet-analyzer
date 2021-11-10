using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class IngredientStatisticDTO
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int CaloricInfoId { get; set; }
    }
}
