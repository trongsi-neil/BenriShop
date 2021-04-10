using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Image
    {
        public string Productid { get; set; }
        public string Imageid { get; set; }
        public string Link { get; set; }

        public virtual Product Product { get; set; }
    }
}
