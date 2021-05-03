using BenriShop.ApiRepository.CartItems;
using BenriShop.Models;
using BenriShop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        // GET: api/CartItems/GetCartItems/userName
        [Authorize(Roles = "Customer")]
        [HttpGet("GetCartItems/{userName}")]
        public async Task<IEnumerable<CartItemView>> GetCartItems(string userName)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.Name != userName)
            {
                return (IEnumerable<CartItemView>)Conflict("Can't access to diffirent account");
            }
            var cartItems = _cartItemRepository.GetCartItems(userName);
            if (cartItems != null)
            {
                return await cartItems;
            }
            return (IEnumerable<CartItemView>)NotFound("Error of GetCartItem");
        }
        /// <summary>
        /// Cập nhật số lượng của 1 sản phẩm trong 1 giỏ hàng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="productId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        // PUT: api/CartItems/UpdateCartItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Customer")]
        [HttpPut("UpdateCartItem")]
        public async Task<IActionResult> UpdateCartItem(CartItem cartItem)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.Name != cartItem.UserName)
            {
                return Conflict("Can't access to diffirent account");
            }

            if (_cartItemRepository.GetCartItem(cartItem.CartItemId) != null)
            {
                try
                {
                    await _cartItemRepository.UpdateCartItem(cartItem);
                    return Ok("Update cartItem successfully");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest("Error when call _cartItemRepository.UpdateCartItem(cartItem)");
                }
            }else
            {
                return BadRequest("This cartItem is not found in database");
            }
            
            
        }
        /// <summary>
        /// Thêm 1 sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        // POST: api/CartItems/AddCartItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[Authorize(Roles = "Customer")]
        [HttpPost("AddCartItem")]
        public async Task<ActionResult<CartItem>> AddCartItem(CartItem cartItem)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.Name != cartItem.UserName)
            {
                return NotFound("Can't accesss to diffirent account");
            }
            try
            {
                cartItem.CartItemId = Guid.NewGuid().ToString();
                await _cartItemRepository.AddCartItem(cartItem);
                return Ok("Add cart item is successful");
            }catch (Exception ex)
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
        // DELETE: api/CartItems/DeleteCartItem/cartItemId
        [Authorize(Roles = "Customer")]
        [HttpDelete("DeleteCartItem/{cartItemId}")]
        public async Task<ActionResult<CartItem>> DeleteCartItem(string cartItemId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var cartItem = await _cartItemRepository.GetCartItem(cartItemId);
            if (identity.Name != cartItem.UserName)
            {
                return NotFound("Can't accesss to diffirent account");
            }
            if (await _cartItemRepository.DeleteCartItem(cartItemId))
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
