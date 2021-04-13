using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Size
    {
        public string SizeId { get; set; }

        public List<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
    }
}
