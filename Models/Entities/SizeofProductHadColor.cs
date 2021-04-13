using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class SizeOfProductHadColor
    {
        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInSizeOfColor { get; set; }

        public Color Color { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
