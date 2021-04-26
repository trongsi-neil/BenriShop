using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Category
    {
        public string CategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
