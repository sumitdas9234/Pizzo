﻿using System.Collections.Generic;
namespace Pizzo.Utilities
{
    //public class Veg
    //{
    //    public string name { get; set; }
    //    public string desc { get; set; }
    //    public int price { get; set; }
    //    public string image { get; set; }
    //}

    //public class Nonveg
    //{
    //    public string name { get; set; }
    //    public string desc { get; set; }
    //    public int price { get; set; }
    //    public string image { get; set; }
    //}

    public class Pizza
    {
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