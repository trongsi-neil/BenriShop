using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class ShippingView
    {
        public string ShippingId { get; set; }

        public int ShippingCost { get; set; }
        public string ShippingFullName { get; set; }
        public string ShipPhoneNumber { get; set; }
        public string ShipAdress { get; set; }
        public OrderView Order { get; set; }

        public string Note { get; set; }
    }
}
