using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStreeTBackend.DTO;
using MyStreeTBackend.Models;
using MyStreeTBackend.Repo;
using MyStreeTBackend.Service;

namespace MyStreeTBackend.controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserRepository _userRepository;

        public ProductController(IProductService productService, IUserRepository userRepository)
        {
            _productService = productService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string brand = null, [FromQuery] string size = null)
        {
            List<Product> products;

            if (!string.IsNullOrEmpty(brand) || !string.IsNullOrEmpty(size))
            {
                products = await _productService.GetProductsFilteredAsync(brand, size);
            }
            else
            {
                products = await _productService.GetAllProductsAsync();
            }

            var response = new ApiResponse<List<Product>>(true, "Products retrieved successfully", products);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                var errorResponse = new ApiResponse<object>(false, "Product not found", null);
                return NotFound(errorResponse);
            }
            var response = new ApiResponse<Product>(true, "Product retrieved successfully", product);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productCreateDto)
        {
            // Check if user is admin
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || !user.IsAdmin)
            {
                var errorResponse = new ApiResponse<object>(false, "Only admins can create products", null);
                return Forbid();
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreateDto.Name,
                Brand = productCreateDto.Brand,
                Price = productCreateDto.Price,
                Size = productCreateDto.Size,
                StockQty = productCreateDto.StockQty,
                ImageUrl = productCreateDto.ImageUrl,
                Description = productCreateDto.Description
            };

            var createdProduct = await _productService.CreateProductAsync(product);
            var response = new ApiResponse<Product>(true, "Product created successfully", createdProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDTO productUpdateDto)
        {
            // Check if user is admin
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || !user.IsAdmin)
            {
                var errorResponse = new ApiResponse<object>(false, "Only admins can update products", null);
                return Forbid();
            }

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                var errorResponse = new ApiResponse<object>(false, "Product not found", null);
                return NotFound(errorResponse);
            }

            existingProduct.Name = productUpdateDto.Name;
            existingProduct.Brand = productUpdateDto.Brand;
            existingProduct.Price = productUpdateDto.Price;
            existingProduct.Size = productUpdateDto.Size;
            existingProduct.StockQty = productUpdateDto.StockQty;
            existingProduct.ImageUrl = productUpdateDto.ImageUrl;
            existingProduct.Description = productUpdateDto.Description;

            var updatedProduct = await _productService.UpdateProductAsync(existingProduct);
            var response = new ApiResponse<Product>(true, "Product updated successfully", updatedProduct);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            // Check if user is admin
            var userIdClaim = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                var errorResponse = new ApiResponse<object>(false, "Unauthorized", null);
                return Unauthorized(errorResponse);
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || !user.IsAdmin)
            {
                var errorResponse = new ApiResponse<object>(false, "Only admins can delete products", null);
                return Forbid();
            }

            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                var errorResponse = new ApiResponse<object>(false, "Product not found", null);
                return NotFound(errorResponse);
            }

            var response = new ApiResponse<object>(true, "Product deleted successfully", null);
            return Ok(response);
        }
    }
}