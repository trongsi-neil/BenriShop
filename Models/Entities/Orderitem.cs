using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public partial class OrderItem
    {
        public string UserName { get; set; }
        public string OrderId { get; set; }
        public string OrderItemId { get; set; }
        public int QuantityInOrder { get; set; }
        public string SizeId { get; set; }
        public string ColorId { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual SizeOfProductHadColor SizeOfProductHadColor { get; set; }
    }
}
