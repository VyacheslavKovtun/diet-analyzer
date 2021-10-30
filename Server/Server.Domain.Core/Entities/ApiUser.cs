using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ApiUser: BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string ApiPassword { get; set; }
        public string Hash { get; set; }
    }
}
