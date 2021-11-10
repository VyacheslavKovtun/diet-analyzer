using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class BaseInfoDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Unit { get; set; }
        public string MealType { get; set; }
    }
}
