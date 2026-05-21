using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStreeTBackend.Models;

namespace MyStreeTBackend.Repo
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<List<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
