using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
            Shipping = new HashSet<Shipping>();
        }

        public string UserName { get; set; }
        public string OrderId { get; set; }
        public bool Payment { get; set; }
        public int Status { get; set; }

        public virtual Account UserNameNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
