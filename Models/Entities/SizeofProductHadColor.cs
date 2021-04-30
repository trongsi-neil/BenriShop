using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class SizeOfProductHadColor
    {
        public SizeOfProductHadColor()
        {
            CartItem = new HashSet<CartItem>();
            OrderItem = new HashSet<OrderItem>();
        }

        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInSizeOfColor { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
