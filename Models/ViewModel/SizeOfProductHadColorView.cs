using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.ViewModel
{
    public class SizeOfProductHadColorView
    {
        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public ProductView Product { get; set; }
        public int QuantityInSizeOfColor { get; set; }
    }
}
