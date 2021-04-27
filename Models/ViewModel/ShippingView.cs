using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class ShippingView
    {
        public string ShippingId { get; set; }

        public int Cost { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
