using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class CurrentProductDTO
    {
        public int Id { get; set; }
        public int BaseInfoId { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
    }
}
