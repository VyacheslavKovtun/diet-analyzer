using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.ConnectingUserResponse
{
    public class ConnectingUserResponse
    {
        public string username { get; set; }
        public string spoonacularPassword { get; set; }
        public string hash { get; set; }
    }
}
