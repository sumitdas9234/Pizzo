using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzo.Utilities
{

        public class Addon
        {
            public string id { get; set; }
            public string name { get; set; }
            public string desc { get; set; }
            public int price { get; set; }
            public string image { get; set; }
        }

        public class AddonItems
        {
            public List<Addon> addons { get; set; }
        }
 
}