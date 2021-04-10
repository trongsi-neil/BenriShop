using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Cartitem
    {
        public string Productid { get; set; }
        public string Username { get; set; }
        public int Quantityincart { get; set; }

        public virtual Product Product { get; set; }
        public virtual Account UsernameNavigation { get; set; }
    }
}
