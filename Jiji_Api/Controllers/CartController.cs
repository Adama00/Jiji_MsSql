using Jiji_Api.Data;
using Jiji_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jiji_Api.Controllers
{
    
    
        [Route("api/[controller]")]
        [ApiController]
        public class CartController : ControllerBase
        {
            private readonly JijiDbContext _context;

            public CartController(JijiDbContext context)
            {
                _context = context;
            }

            // GET: api/Cart
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
            {
                return await _context.Cart.ToListAsync();
            }

            // GET: api/Cart/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Cart>> GetCartItem(int id)
            {
                var cartItem = await _context.Cart.FindAsync(id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                return cartItem;
            }

            // PUT: api/Cart/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutCartItem(int id, Cart cartItem)
            {
                if (id != cartItem.Id)
                {
                    return BadRequest();
                }

                _context.Entry(cartItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/Cart
            [HttpPost]
            public async Task<ActionResult<Cart>> PostCartItem(Cart cartItem)
            {
                _context.Cart.Add(cartItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
            }

            // DELETE: api/Cart/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCartItem(int id)
            {
                var cartItem = await _context.Cart.FindAsync(id);
                if (cartItem == null)
                {
                    return NotFound();
                }
                _context.Cart.Remove(cartItem);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool CartItemExists(int id)
            {
                return _context.Cart.Any(e => e.Id == id);
            }
        }

    }
        



   