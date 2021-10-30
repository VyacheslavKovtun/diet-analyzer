using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class IngredientStatistic: BaseEntity<int>
    {
        public int IngredientId { get; set; }
        public int CaloricInfoId { get; set; }
    }
}
