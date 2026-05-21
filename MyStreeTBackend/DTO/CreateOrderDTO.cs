using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStreeTBackend.DTO
{
    public class CreateOrderDTO
    {
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; } // COD, UPI_MOCK
        public List<CartItemDTO> CartItems { get; set; }
    }

    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string SelectedSize { get; set; }
    }
}
