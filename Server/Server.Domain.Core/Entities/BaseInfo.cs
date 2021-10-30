using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class BaseInfo: BaseEntity<int>
    {
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Unit { get; set; }
        public string MealType { get; set; }
    }
}
