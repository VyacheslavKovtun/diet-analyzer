using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class CurrentIngredient: BaseEntity<int>
    {
        public int IngredientId { get; set; }
        public Guid UserId { get; set; }
        public int BaseInfoId { get; set; }
    }
}
