using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class OrderView
    {
        public string OrderId { get; set; }
        public string UserName { get; set; }

        public DateTime OrderDate { set; get; }
        public bool Payment { get; set; }

        public string Status { get; set; }

        public ICollection<OrderItemView> OrderItems { get; set; }

        public ShippingView Shippings { get; set; }
    }
}
