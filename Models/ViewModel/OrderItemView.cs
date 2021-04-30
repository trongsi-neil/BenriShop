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

        public string OrderItemId { get; set; }
        
        public int QuantityInOrder { get; set; }

        public string SizeId { get; set; }

        public string ColorId { get; set; }

    }
}
