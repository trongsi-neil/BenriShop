using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class CartItem
    {
        public int ProductId { get; set; }

        public string UserName { get; set; }
        
        public int QuantityInCart { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public Product Product { get; set; }
       
        public Account Account { get; set; }
    }
}
