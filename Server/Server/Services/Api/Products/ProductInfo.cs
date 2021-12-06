using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.Products
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Ingredient
    {
        public string description { get; set; }
        public string name { get; set; }
        public string safety_level { get; set; }
    }

    public class Nutrient
    {
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public double percentOfDailyNeeds { get; set; }
    }

    public class CaloricBreakdown
    {
        public double percentProtein { get; set; }
        public double percentFat { get; set; }
        public double percentCarbs { get; set; }
    }

    public class Nutrition
    {
        public List<Nutrient> nutrients { get; set; }
        public CaloricBreakdown caloricBreakdown { get; set; }
    }

    public class Servings
    {
        public double number { get; set; }
        public double? size { get; set; }
        public string unit { get; set; }
    }

    public class ProductRoot
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<string> breadcrumbs { get; set; }
        public string imageType { get; set; }
        public List<string> badges { get; set; }
        public List<string> importantBadges { get; set; }
        public int ingredientCount { get; set; }
        public object generatedText { get; set; }
        public string ingredientList { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public int likes { get; set; }
        public string aisle { get; set; }
        public Nutrition nutrition { get; set; }
        public double price { get; set; }
        public Servings servings { get; set; }
        public double? spoonacularScore { get; set; }
    }
}
