using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Color
    {
        public Color()
        {
            SizeOfProductHadColor = new HashSet<SizeOfProductHadColor>();
        }

        public string ColorId { get; set; }

        public virtual ICollection<SizeOfProductHadColor> SizeOfProductHadColor { get; set; }
    }
}
