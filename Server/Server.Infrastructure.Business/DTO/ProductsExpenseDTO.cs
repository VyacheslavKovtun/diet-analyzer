using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class ProductsExpenseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int BaseInfoId { get; set; }
        public DateTime PurchasingDate { get; set; }
        public bool IsExpired { get; set; }
    }
}
