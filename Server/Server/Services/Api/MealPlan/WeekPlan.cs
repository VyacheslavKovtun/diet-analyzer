using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.MealPlan
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Nutrient
    {
        public string name { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
        public int percentOfDailyNeeds { get; set; }
    }

    public class NutritionSummary
    {
        public List<Nutrient> nutrients { get; set; }
    }

    public class NutritionSummaryBreakfast
    {
        public List<Nutrient> nutrients { get; set; }
    }

    public class NutritionSummaryLunch
    {
        public List<Nutrient> nutrients { get; set; }
    }

    public class NutritionSummaryDinner
    {
        public List<Nutrient> nutrients { get; set; }
    }

    public class Ingredient
    {
        public string name { get; set; }
        public string unit { get; set; }
        public string amount { get; set; }
        public string image { get; set; }
    }

    public class Value
    {
        public int servings { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string imageType { get; set; }
        public string image { get; set; }
        public List<Ingredient> ingredients { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public int slot { get; set; }
        public int position { get; set; }
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Day
    {
        public NutritionSummary nutritionSummary { get; set; }
        public NutritionSummaryBreakfast nutritionSummaryBreakfast { get; set; }
        public NutritionSummaryLunch nutritionSummaryLunch { get; set; }
        public NutritionSummaryDinner nutritionSummaryDinner { get; set; }
        public int date { get; set; }
        public string day { get; set; }
        public List<Item> items { get; set; }
    }

    public class WeekPlan
    {
        public List<Day> days { get; set; }
    }
}
