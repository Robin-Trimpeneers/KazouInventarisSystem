﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazouInventaris.Classes
{
   public class inventoryItem
    {
        public int id {get;set;}        
        public string name { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string location { get; set; }
        public int category { get; set; }
        public int purchaseAmount { get; set; }
    }
}
