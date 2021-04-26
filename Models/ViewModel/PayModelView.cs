using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeWebApp.Models
{
    public class PayModelView
    {
        public int Total { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvc { get; set; }
    }
}
