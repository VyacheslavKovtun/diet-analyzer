﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.Recipes
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ProductMatch
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string imageUrl { get; set; }
        public double averageRating { get; set; }
        public double ratingCount { get; set; }
        public double score { get; set; }
        public string link { get; set; }
    }

    public class WinePairing
    {
        public List<string> pairedWines { get; set; }
        public string pairingText { get; set; }
        public List<ProductMatch> productMatches { get; set; }
    }

    public class RecipeRoot
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string imageType { get; set; }
        public int servings { get; set; }
        public int readyInMinutes { get; set; }
        public string license { get; set; }
        public string sourceName { get; set; }
        public string sourceUrl { get; set; }
        public string spoonacularSourceUrl { get; set; }
        public int aggregateLikes { get; set; }
        public double healthScore { get; set; }
        public double spoonacularScore { get; set; }
        public double pricePerServing { get; set; }
        public List<object> analyzedInstructions { get; set; }
        public bool cheap { get; set; }
        public string creditsText { get; set; }
        public List<object> cuisines { get; set; }
        public bool dairyFree { get; set; }
        public List<object> diets { get; set; }
        public string gaps { get; set; }
        public bool glutenFree { get; set; }
        public string instructions { get; set; }
        public bool ketogenic { get; set; }
        public bool lowFodmap { get; set; }
        public List<object> occasions { get; set; }
        public bool sustainable { get; set; }
        public bool vegan { get; set; }
        public bool vegetarian { get; set; }
        public bool veryHealthy { get; set; }
        public bool veryPopular { get; set; }
        public bool whole30 { get; set; }
        public int weightWatcherSmartPoints { get; set; }
        public List<string> dishTypes { get; set; }
        public List<ExtendedIngredient> extendedIngredients { get; set; }
        public string summary { get; set; }
        public WinePairing winePairing { get; set; }
    }
}
