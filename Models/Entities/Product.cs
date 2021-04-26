using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int StorageQuantity { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<HaveTag> HaveTags { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
    }
}
