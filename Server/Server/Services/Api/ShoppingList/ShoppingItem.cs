using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.ShoppingList
{
    public class ShoppingItem
    {
        public int? id { get; set; }
        public string item { get; set; }
        public string? aisle { get; set; }
        public bool parse { get; set; }
    }
}
