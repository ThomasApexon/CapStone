using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "PLACED"; // PLACED, CONFIRMED, SHIPPED, DELIVERED
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; } // COD, UPI_MOCK
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
