using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Infrastructure.Business.DTO
{
    public class CaloricInfoDTO
    {
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Protein { get; set; }
    }
}
