using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class ForbiddenIngredientDTO
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public Guid UserId { get; set; }
    }
}
