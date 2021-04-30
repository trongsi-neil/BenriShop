using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class CartItem
    {
        public string UserName { get; set; }
        public string CartItemId { get; set; }
        public int QuantityInCart { get; set; }
        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public int ProductId { get; set; }

        public virtual SizeOfProductHadColor SizeOfProductHadColor { get; set; }
        public virtual Account UserNameNavigation { get; set; }
    }
}
