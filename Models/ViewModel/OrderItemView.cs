using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class OrderItemView
    {
        public int ProductId { get; set; }
        
        public string OrderId { get; set; }
        
        public int QuantityInOrder { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

    }
}
