using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzo.Utilities
{
   
        public class Pizza
        {
            public string id { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public int price { get; set; }
            public string image { get; set; }
        }

        public class MenuItem
        {
            public List<Pizza> veg { get; set; }
            public List<Pizza> nonveg { get; set; }
        }
    
}