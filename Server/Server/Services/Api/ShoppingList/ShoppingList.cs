using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.ShoppingList
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Original
    {
        public double amount { get; set; }
        public string unit { get; set; }
    }

    public class Metric
    {
        public double amount { get; set; }
        public string unit { get; set; }
    }

    public class Us
    {
        public double amount { get; set; }
        public string unit { get; set; }
    }

    public class Measures
    {
        public Original original { get; set; }
        public Metric metric { get; set; }
        public Us us { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public Measures measures { get; set; }
        public bool pantryItem { get; set; }
        public string aisle { get; set; }
        public double cost { get; set; }
        public int ingredientId { get; set; }
    }

    public class Aisle
    {
        public string aisle { get; set; }
        public List<Item> items { get; set; }
    }

    public class ShoppingList
    {
        public List<Aisle> aisles { get; set; }
        public double cost { get; set; }
        public int startDate { get; set; }
        public int endDate { get; set; }
    }
}
