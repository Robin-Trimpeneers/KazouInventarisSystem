using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazouInventaris.Classes
{
    public class BorrowedItems
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string vacantieCode { get; set; }
        public string Date { get; set; }
        public int uitgeleendItemID { get; set; }
        public int amount { get; set; }
        public int returned { get; set; }
    }
}
