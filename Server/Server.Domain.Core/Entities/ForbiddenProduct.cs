using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ForbiddenProduct: BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
