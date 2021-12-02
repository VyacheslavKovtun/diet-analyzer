using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.Ingredients
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class EstimatedCost
    {
        public double value { get; set; }
        public string unit { get; set; }
    }

    public class Nutrient
    {
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public double percentOfDailyNeeds { get; set; }
    }

    public class Property
    {
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
    }

    public class CaloricBreakdown
    {
        public double percentProtein { get; set; }
        public double percentFat { get; set; }
        public double percentCarbs { get; set; }
    }

    public class WeightPerServing
    {
        public int amount { get; set; }
        public string unit { get; set; }
    }

    public class Nutrition
    {
        public List<Nutrient> nutrients { get; set; }
        public List<Property> properties { get; set; }
        public CaloricBreakdown caloricBreakdown { get; set; }
        public WeightPerServing weightPerServing { get; set; }
    }

    public class IngredientRoot
    {
        public int id { get; set; }
        public string original { get; set; }
        public string originalName { get; set; }
        public string name { get; set; }
        public string nameClean { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public string unitShort { get; set; }
        public string unitLong { get; set; }
        public List<string> possibleUnits { get; set; }
        public EstimatedCost estimatedCost { get; set; }
        public string consistency { get; set; }
        public List<string> shoppingListUnits { get; set; }
        public string aisle { get; set; }
        public string image { get; set; }
        public List<object> meta { get; set; }
        public Nutrition nutrition { get; set; }
        public List<string> categoryPath { get; set; }
    }
}
