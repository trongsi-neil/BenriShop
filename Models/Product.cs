using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Product
    {
        public Product()
        {
            Cartitem = new HashSet<Cartitem>();
            HaveTag = new HashSet<HaveTag>();
            Image = new HashSet<Image>();
            Orderitem = new HashSet<Orderitem>();
            Sizeofproducthadcolor = new HashSet<Sizeofproducthadcolor>();
        }

        public string Productid { get; set; }
        public string Categoryid { get; set; }
        public string Productname { get; set; }
        public string Productdescription { get; set; }
        public int Price { get; set; }
        public int Storagequantity { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Cartitem> Cartitem { get; set; }
        public virtual ICollection<HaveTag> HaveTag { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<Orderitem> Orderitem { get; set; }
        public virtual ICollection<Sizeofproducthadcolor> Sizeofproducthadcolor { get; set; }
    }
}
