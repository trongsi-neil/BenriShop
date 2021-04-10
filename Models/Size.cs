using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Size
    {
        public Size()
        {
            Sizeofproducthadcolor = new HashSet<Sizeofproducthadcolor>();
        }

        public string Sizeid { get; set; }

        public virtual ICollection<Sizeofproducthadcolor> Sizeofproducthadcolor { get; set; }
    }
}
