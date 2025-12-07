using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public short UserRole { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        // Navigation
        public ICollection<Address> Addresses { get; set; }
        public ICollection<PaymentMethod> PaymentMethods { get; set; }
       
    }
}
