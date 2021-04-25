using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Color
    {
        public string ColorId { get; set; }

        public virtual ICollection<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
    }
}
