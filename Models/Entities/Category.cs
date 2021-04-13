using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Category
    {
        public string CategoryId { get; set; }

        public List<Product> Products { get; set; }
    }
}
