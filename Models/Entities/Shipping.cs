using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Shipping
    {
        public string ShipPhoneNumber { get; set; }
        public string ShipFullName { get; set; }
        public string ShipAddress { get; set; }
        public int ShippingCost { get; set; }
        public string Note { get; set; }
        public string ShippingId { get; set; }
        public string UserName { get; set; }
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
