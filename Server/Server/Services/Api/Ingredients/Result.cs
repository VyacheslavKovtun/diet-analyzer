using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.Ingredients
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }

    public class Root
    {
        public List<Result> results { get; set; }
        public int offset { get; set; }
        public int number { get; set; }
        public int totalResults { get; set; }
    }
}
