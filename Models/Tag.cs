using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class Tag
    {
        public Tag()
        {
            HaveTag = new HashSet<HaveTag>();
        }

        public string Tagid { get; set; }

        public virtual ICollection<HaveTag> HaveTag { get; set; }
    }
}
