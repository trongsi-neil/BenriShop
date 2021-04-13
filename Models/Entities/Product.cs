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
        public List<CartItem> CartItems { get; set; }
        public List<HaveTag> HaveTags { get; set; }
        public List<Image> Images { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<SizeOfProductHadColor> SizeOfProductHadColors { get; set; }
    }
}
