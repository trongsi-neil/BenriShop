using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Tag
    {
        public string TagId { get; set; }

        public virtual ICollection<HaveTag> HaveTags { get; set; }
    }
}
