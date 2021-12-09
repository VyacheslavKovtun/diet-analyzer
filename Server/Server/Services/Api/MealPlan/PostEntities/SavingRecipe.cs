using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services.Api.MealPlan.PostEntities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Value
    {
        public int id { get; set; }
        public int servings { get; set; }
        public string title { get; set; }
        public string imageType { get; set; }
    }

    public class SavingRecipe
    {
        public long date { get; set; }
        public int slot { get; set; }
        public int position { get; set; }
        public string type { get; set; }
        public Value value { get; set; }
    }


}
