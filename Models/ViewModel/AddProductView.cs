using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class AddProductView
    {
        public Product Product { get; set; }
        public ICollection<HaveTag> HaveTags { get; set; }
        public ICollection<SizeOfProductHadColor> SizeOfProductHadColors {get; set;}
    }
}
