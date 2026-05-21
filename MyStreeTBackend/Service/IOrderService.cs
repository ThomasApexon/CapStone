using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Service
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Guid userId, CreateOrderDTO createOrderDto);
        Task<Order> GetOrderAsync(Guid orderId);
        Task<List<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order> UpdateOrderStatusAsync(Guid orderId, string status);
    }
}
