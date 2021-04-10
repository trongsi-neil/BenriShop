using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class HaveTag
    {
        public string Productid { get; set; }
        public string Tagid { get; set; }

        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
