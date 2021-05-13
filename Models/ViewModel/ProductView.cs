using System;
using System.Collections.Generic;

namespace BenriShop.Models.ViewModel
{
    public class ProductView
    {

        public int ProductId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int StorageQuantity { get; set; }

        public Boolean IsDisable { get; set; }

        public List<HaveTagView> HaveTags { get; set; }
        public List<ImageView> Images { get; set; }
        public List<SizeOfProductHadColorView> SizeOfProductHadColors { get; set; }
    }
}
