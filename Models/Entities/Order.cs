using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Order
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }

        public DateTime OrderDate { set; get; }
        public bool Payment { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
