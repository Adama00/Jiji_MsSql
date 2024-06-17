using System;
using System.Collections.Generic;
using System.Linq;
using Jiji_Api.Data;
using Jiji_Api.Models;
using Jiji_Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Jiji_Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class JijiController : ControllerBase

    {
        private readonly JijiDbContext _context;
        private readonly IProductsService _productsService;
        public JijiController(JijiDbContext context, IProductsService productsService)
        {
            JijiDbContext _context = context;
            IProductsService _productsService = productsService;

        }
       

        [HttpGet("allProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving products: {ex.Message}");
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] int? categoryId, [FromQuery] int? regionId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var yeti = await _productsService.GetProducts(categoryId, regionId, minPrice, maxPrice);
            return StatusCode(int.Parse(yeti.Code), yeti);
        }

        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving product details: {ex.Message}");
            }
        }

        [HttpPost("cart")]
        public IActionResult AddToCart([FromBody] Cart cartItem)
        {
            try
            {
                _context.Cart.Add(cartItem);
                _context.SaveChanges();
                return Ok("Product added to cart successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding product to cart: {ex.Message}");
            }
        }

        // Implement other endpoints here...
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart()
        {
            var yeti = await _productsService.GetCart();
            return StatusCode(int.Parse(yeti.Code), yeti);
        }

       

        // Update the quantity of a product in the cart
        [HttpPut("cart/{id}")]
        public IActionResult UpdateCartItem(int id, [FromBody] int quantity)
        {
            try
            {
                var cartItem = _context.Cart.Find(id);
                if (cartItem == null)
                {
                    return NotFound("Cart item not found");
                }

                cartItem.Quantity = quantity;
                _context.SaveChanges();
                return Ok("Cart item updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating cart item: {ex.Message}");
            }
        }

        // Remove a product from the cart
        [HttpDelete("cart/{id}")]
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                var cartItem = _context.Cart.Find(id);
                if (cartItem == null)
                {
                    return NotFound("Cart item not found");
                }

                _context.Cart.Remove(cartItem);
                _context.SaveChanges();
                return Ok("Cart item removed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing cart item: {ex.Message}");
            }
        }

        // Check the stock quantity of a specific product
        [HttpGet("stock/{id}")]
        public IActionResult GetStock(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound("Product not found");
                }

                return Ok(new { product.Id, product.StockQuantity });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while checking stock quantity: {ex.Message}");
            }
        }



    }

}
