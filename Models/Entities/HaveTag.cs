using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class HaveTag
    {
        public int ProductId { get; set; }
        public string TagId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
