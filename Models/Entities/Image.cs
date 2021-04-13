using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Image
    {
        public int ProductId { get; set; }
        public string Imageid { get; set; }
        public string Link { get; set; }

        public Product Product { get; set; }
    }
}
