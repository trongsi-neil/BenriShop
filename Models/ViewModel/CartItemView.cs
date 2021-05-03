using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class CartItemView
    {
        public string UserName { get; set; }
        public string CartItemId { get; set; }
        public int QuantityInCart { get; set; }
        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public int ProductId { get; set; }
        public ProductView ProductView { get; set; }
    }
}
