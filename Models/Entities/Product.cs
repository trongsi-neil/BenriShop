using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Product
    {
        public Product()
        {
            HaveTag = new HashSet<HaveTag>();
            Image = new HashSet<Image>();
            SizeOfProductHadColor = new HashSet<SizeOfProductHadColor>();
        }

        public int ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int StorageQuantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<HaveTag> HaveTag { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<SizeOfProductHadColor> SizeOfProductHadColor { get; set; }
    }
}
