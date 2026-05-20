using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyStreeTBackend.Models;
using MyStreeTBackend.Service;

namespace MyStreeTBackend.controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var response = new ApiResponse<IEnumerable<Product>>(true, "Products retrieved successfully", products);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponse<string>(false, "Product not found", null));
            }
            var response = new ApiResponse<Product>(true, "Product retrieved successfully", product);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var result = await _productService.AddProductAsync(product);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>(false, "Failed to add product", null));
            }
            var response = new ApiResponse<string>(true, "Product added successfully", null);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new ApiResponse<string>(false, "Product ID mismatch", null));
            }
            var result = await _productService.UpdateProductAsync(product);
            if (!result)
            {
                return BadRequest(new ApiResponse<string>(false, "Failed to update product", null));
            }
            var response = new ApiResponse<string>(true, "Product updated successfully", null);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<string>(false, "Product not found", null));
            }
            var response = new ApiResponse<string>(true, "Product deleted successfully", null);
            return Ok(response);
        }

        
    }
}