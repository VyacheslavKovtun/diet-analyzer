using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.Products
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string imageType { get; set; }
    }

    public class Root
    {
        public List<Product> products { get; set; }
        public int totalProducts { get; set; }
        public string type { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
    }
}
