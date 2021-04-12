using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Orderitem
    {
        public int Productid { get; set; }
        public string Orderid { get; set; }
        public int Quantityinorder { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
