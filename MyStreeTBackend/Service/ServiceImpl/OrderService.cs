using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;

namespace MyStreeTBackend.Service.ServiceImpl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> CreateOrderAsync(Guid userId, CreateOrderDTO createOrderDto)
        {
            if (createOrderDto.CartItems == null || createOrderDto.CartItems.Count == 0)
            {
                throw new ArgumentException("Cart cannot be empty");
            }

            // Validate products and calculate total
            decimal totalPrice = 0;
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in createOrderDto.CartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {cartItem.ProductId} not found");
                }

                if (product.StockQty < cartItem.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for {product.Name}");
                }

                var itemPrice = product.Price * cartItem.Quantity;
                totalPrice += itemPrice;

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = product.Price,
                    SelectedSize = cartItem.SelectedSize
                };

                orderItems.Add(orderItem);

                // Reduce stock
                product.StockQty -= cartItem.Quantity;
                await _productRepository.UpdateProductAsync(product);
            }

            // Create order
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                TotalPrice = totalPrice,
                Status = "PLACED",
                ShippingAddress = createOrderDto.ShippingAddress,
                PaymentMethod = createOrderDto.PaymentMethod,
                OrderItems = orderItems
            };

            return await _orderRepository.CreateOrderAsync(order);
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found");
            }
            return order;
        }

        public async Task<List<Order>> GetUserOrdersAsync(Guid userId)
        {
            return await _orderRepository.GetUserOrdersAsync(userId);
        }

        public async Task<Order> UpdateOrderStatusAsync(Guid orderId, string status)
        {
            var validStatuses = new[] { "PLACED", "CONFIRMED", "SHIPPED", "DELIVERED" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException($"Invalid status. Must be one of: {string.Join(", ", validStatuses)}");
            }

            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {orderId} not found");
            }

            order.Status = status;
            return await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
