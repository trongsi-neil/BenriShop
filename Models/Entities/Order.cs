using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Order
    {

        public string OrderId { get; set; }
        public string UserName { get; set; }
        public bool Payment { get; set; }

        public Account Account { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Shipping> Shippings { get; set; }
    }
}
