using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Shipping
    {
        public string Orderid { get; set; }
        public string Shippingid { get; set; }
        public int Cost { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }

        public virtual Order Order { get; set; }
    }
}
