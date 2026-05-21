using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }

    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SelectedSize { get; set; }
    }
}
