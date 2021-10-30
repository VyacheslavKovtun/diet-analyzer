using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ProductStatistic: BaseEntity<int>
    {
        public int ProductId { get; set; }
        public int CaloricInfoId { get; set; }
    }
}
