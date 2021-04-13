using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Tag
    {
        public string TagId { get; set; }

        public List<HaveTag> HaveTags { get; set; }
    }
}
