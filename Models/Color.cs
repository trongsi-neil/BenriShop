using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Color
    {
        public Color()
        {
            Sizeofproducthadcolor = new HashSet<Sizeofproducthadcolor>();
        }

        public string Colorid { get; set; }

        public virtual ICollection<Sizeofproducthadcolor> Sizeofproducthadcolor { get; set; }
    }
}
