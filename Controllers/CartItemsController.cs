using BenriShop.ApiRepository.CartItems;
using BenriShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemsController(ICartItemRepository cartItemRepository)
        {
            this._cartItemRepository = cartItemRepository;
        }
        /// <summary>
        /// Lấy tất cả sản phẩm có trong giỏ hàng của một tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET: api/CartItems
        [HttpGet("{userName}")]
        public async Task<IEnumerable<CartItem>> GetCartItems(string userName)
        {
            var cartItems = _cartItemRepository.GetCartItems(userName);
            if (cartItems != null)
            {
                return await cartItems;
            }
            return (IEnumerable<CartItem>)NotFound("Error of GetCartItem");
        }

        //GET: api/CartItems/5
        /*[HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return cartItem;
        }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem cartItem)
        {
            if (id != cartItem.ProductId)
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

        // POST: api/CartItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CartItem>> PostCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartItemExists(cartItem.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCartItem", new { id = cartItem.ProductId }, cartItem);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}/{UserName}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(int id, string UserName)
        {
            if(await _cartItemRepository.DeleteCartItem(UserName, id))
            {
                return Ok("Delete CartItem successfully!(" + UserName + ", " + id);
            }else
            {
                return BadRequest("Delete CartItem failed!");
            }
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.ProductId == id);
        }*/
    }
}
