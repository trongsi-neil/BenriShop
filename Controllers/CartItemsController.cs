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
        /// <summary>
        /// Cập nhật số lượng của 1 sản phẩm trong 1 giỏ hàng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="productId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
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
                return BadRequest("Error when call _cartItemRepository.UpdateCartItem(cartItem)");
            }
            
        }
        /// <summary>
        /// Thêm 1 sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        // POST: api/CartItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CartItem>> AddCartItem(CartItem cartItem)
        {
            try
            {
                await _cartItemRepository.AddCartItem(cartItem);
                return CreatedAtAction("GetCartItems", new { userName = cartItem.UserName }, cartItem);
            }catch
            {
                return BadRequest("Error when call _cartItemRepository.AddCartItem(cartItem)");
            }
            
        }
        /// <summary>
        /// Xóa 1 sản phẩm khỏi giỏ hàng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        // DELETE: api/CartItems/5
        [HttpDelete("{userName}/{productId}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(string userName, int productId)
        {
            if(await _cartItemRepository.DeleteCartItem(userName, productId))
            {
                return Ok("Delete successfully!");
            }
            else
            {
                return BadRequest("Error when call _cartItemRepository.DeleteCartItem(userName, productId)");
            }
        }

        /*private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.ProductId == id);
        }*/
    }
}
