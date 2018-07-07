using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzo.Utilities
{

    public class AddOn
    {
        public string id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public int price { get; set; }
        public string image { get; set; }
    }

    public class AddOnMenuItem
    {
        public List<AddOn> veg { get; set; }
        public List<AddOn> nonveg { get; set; }
    }

}