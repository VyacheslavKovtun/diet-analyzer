using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class CurrentProduct: BaseEntity<int>
    {
        public int BaseInfoId { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
    }
}
