using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ProductsExpense: BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int BaseInfoId { get; set; }
        public DateTime PurchasingDate { get; set; }
        public bool IsExpired { get; set; }
    }
}
