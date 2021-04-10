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
            Cartitem = new HashSet<Cartitem>();
            Order = new HashSet<Order>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Cartitem> Cartitem { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
