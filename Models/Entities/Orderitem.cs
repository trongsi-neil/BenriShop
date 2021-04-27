﻿using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class OrderItem
    {
        public int ProductId { get; set; }
        public string OrderId { get; set; }
        public int QuantityInOrder { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}