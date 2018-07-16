using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzo.Utilities
{
    public class Orders
    {
        public static List<Order> CurrentOrder;

        public void AddOrder(string _name, int _price)
        {
            Order item = new Order
            {
                name = _name,
                price = _price,
                quantity = 1
            };
            CurrentOrder.Add(item);
        }
    }
    public class Order
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }

}