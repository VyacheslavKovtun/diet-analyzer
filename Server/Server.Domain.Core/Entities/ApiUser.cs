using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class ApiUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string ApiPassword { get; set; }
        public string Hash { get; set; }
    }
}
