using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public string Categoryid { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
