using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;
using BenriShop.Models.ViewModel;

namespace BenriShop.ApiRepository.CartItems
{
    public interface ICartItemRepository
    {
        /// <summary>
        /// Lấy tất cả sản phẩm có trong 1 giỏ hàng bằng cách truyền vào username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<IEnumerable<CartItemView>> GetCartItems(string userName);
        /// <summary>
        /// Lấy 1 sản phẩm trong giỏ hàng bằng cách truyền vào username và ProductId
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<CartItem> GetCartItem(string cartItemId);
        /// <summary>
        /// Thêm 1 sản phẩm vào giỏ hàng bằng cách truyền vào 1 đối tượng CartItem
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public Task<CartItem> AddCartItem(CartItem cartItem);
        /// <summary>
        /// Cập nhật số lượng 1 sản phẩm trong giỏ hàng bằng cách truyền vào 1 đối tượng CartItem với value khác
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public Task<CartItem> UpdateCartItem(CartItem cartItem);
        /// <summary>
        /// Xóa 1 sản phẩm khỏi giỏ hàng bằng cách truyền vào username và ProductId
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<bool> DeleteCartItem(string cartItemId);
        /// <summary>
        /// Chuyển tất cả sản phẩm của giỏ hàng sang 1 đơn hàng để tiến hành đặt hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /*public Task<bool> AddItemsFromCartToOrder(string orderId);*/
    }
}
