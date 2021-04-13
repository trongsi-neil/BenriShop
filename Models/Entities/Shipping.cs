using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Shipping
    {
        public string ShippingId { get; set; }
        public string OrderId { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }

        public Order Order { get; set; }
    }
}
