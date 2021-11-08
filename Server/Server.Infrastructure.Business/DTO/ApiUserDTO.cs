using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class ApiUserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string ApiPassword { get; set; }
        public string Hash { get; set; }
    }
}
