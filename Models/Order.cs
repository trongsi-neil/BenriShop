using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderitem = new HashSet<Orderitem>();
            Shipping = new HashSet<Shipping>();
        }

        public string Orderid { get; set; }
        public string Username { get; set; }
        public bool Payment { get; set; }

        public virtual Account UsernameNavigation { get; set; }
        public virtual ICollection<Orderitem> Orderitem { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
