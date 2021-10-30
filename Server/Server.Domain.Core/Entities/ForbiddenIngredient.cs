using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ForbiddenIngredient: BaseEntity<int>
    {
        public int IngredientId { get; set; }
        public Guid UserId { get; set; }
    }
}
