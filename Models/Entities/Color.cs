using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Color
    {
        public string ColorId { get; set; }

        public List<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
    }
}
