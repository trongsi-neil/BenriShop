using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Size
    {
        public Size()
        {
            SizeOfProductHadColor = new HashSet<SizeOfProductHadColor>();
        }

        public string SizeId { get; set; }

        public virtual ICollection<SizeOfProductHadColor> SizeOfProductHadColor { get; set; }
    }
}
