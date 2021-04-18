using BenriShop.ApiRepository.CartItems;
using BenriShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // GET: api/CartItems/userName
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

        // PUT: api/CartItems/userName/1
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{userName}/{productId}")]
        public async Task<IActionResult> UpdateCartItem(string userName, int productId, CartItem cartItem)
        {
            if (productId != cartItem.ProductId || userName != cartItem.UserName)
            {
                return BadRequest("Wrong of productId or userName");
            }

            var _product = await _cartItemRepository.GetCartItem(cartItem.UserName, cartItem.ProductId);

            try
            {
                await _cartItemRepository.UpdateCartItem(cartItem);
                return Ok("Update cartItem successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }
            
        }

        // POST: api/CartItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CartItem>> AddCartItem(CartItem cartItem)
        {
            await _cartItemRepository.AddCartItem(cartItem);

            return CreatedAtAction("GetCartItems", new { userName = cartItem.UserName }, cartItem);
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{userName}/{productId}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(string userName, int productId)
        {
            if(await _cartItemRepository.DeleteCartItem(userName, productId))
            {
                return Ok("Delete CartItem successfully!(" + userName + ", " + productId);
            }else
            {
                return BadRequest("Delete CartItem failed!");
            }
        }

        /*private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.ProductId == id);
        }*/
    }
}
