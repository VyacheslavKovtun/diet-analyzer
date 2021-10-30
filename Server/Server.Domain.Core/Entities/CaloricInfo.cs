using Server.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Domain.Core.Entities
{
    public class CaloricInfo: BaseEntity<int>
    {
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Protein { get; set; }
    }
}
