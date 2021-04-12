using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Sizeofproducthadcolor
    {
        public string Sizeid { get; set; }
        public string Colorid { get; set; }
        public int Productid { get; set; }
        public int Quantityinsizeofcolor { get; set; }

        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
    }
}
