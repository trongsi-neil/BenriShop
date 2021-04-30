using System;
using System.Collections.Generic;

namespace BenriShop.Models
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Mod = "Mod";
        public const string Customer = "Customer";
    }
    public partial class Account
    {

        public Account()
        {
            CartItem = new HashSet<CartItem>();
            Order = new HashSet<Order>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
