using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Service;

namespace MyStreeTBackend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
        {
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(userId, createOrderDto);
                var response = new ApiResponse<OrderDTO>(
                    true, 
                    "Order created successfully", 
                    MapOrderToDto(order)
                );
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, response);
            }
            catch (ArgumentException ex)
            {
                var errorResponse = new ApiResponse<object>(false, ex.Message, null, new List<string> { ex.Message });
                return BadRequest(errorResponse);
            }
            catch (KeyNotFoundException ex)
            {
                var errorResponse = new ApiResponse<object>(false, ex.Message, null, new List<string> { ex.Message });
                return NotFound(errorResponse);
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = new ApiResponse<object>(false, ex.Message, null, new List<string> { ex.Message });
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            var orders = await _orderService.GetUserOrdersAsync(userId);
            var orderDtos = orders.Select(MapOrderToDto).ToList();
            var response = new ApiResponse<List<OrderDTO>>(true, "Orders retrieved successfully", orderDtos);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            try
            {
                var order = await _orderService.GetOrderAsync(id);
                
                // Verify user owns this order
                if (order.UserId != userId)
                {
                    var errorResponse = new ApiResponse<object>(false, "You can only view your own orders", null);
                    return Forbid();
                }

                var response = new ApiResponse<OrderDTO>(true, "Order retrieved successfully", MapOrderToDto(order));
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                var errorResponse = new ApiResponse<object>(false, ex.Message, null, new List<string> { ex.Message });
                return NotFound(errorResponse);
            }
        }

        private OrderDTO MapOrderToDto(Order order)
        {
            return new OrderDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                ShippingAddress = order.ShippingAddress,
                PaymentMethod = order.PaymentMethod,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    SelectedSize = oi.SelectedSize
                }).ToList()
            };
        }
    }
}
