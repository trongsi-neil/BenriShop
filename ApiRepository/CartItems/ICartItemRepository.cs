using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.ApiRepository.CartItems
{
    public interface ICartItemRepository
    {
        /// <summary>
        /// Lấy tất cả sản phẩm có trong 1 giỏ hàng bằng cách truyền vào username
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Task<IEnumerable<CartItem>> GetCartItems(string UserName);
        /// <summary>
        /// Lấy 1 sản phẩm trong giỏ hàng bằng cách truyền vào username và ProductId
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Task<CartItem> GetCartItem(string UserName, int ProductId);
        /// <summary>
        /// Thêm 1 sản phẩm vào giỏ hàng bằng cách truyền vào 1 đối tượng CartItem
        /// </summary>
        /// <param name="CartItem"></param>
        /// <returns></returns>
        public Task<CartItem> AddCartItem(CartItem CartItem);
        /// <summary>
        /// Cập nhật số lượng 1 sản phẩm trong giỏ hàng bằng cách truyền vào 1 đối tượng CartItem với value khác
        /// </summary>
        /// <param name="CartItem"></param>
        /// <returns></returns>
        public Task<CartItem> UpdateCartItem(CartItem CartItem);
        /// <summary>
        /// Xóa 1 sản phẩm khỏi giỏ hàng bằng cách truyền vào username và ProductId
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Task<bool> DeleteCartItem(string UserName, int ProductId);
        /// <summary>
        /// Chuyển tất cả sản phẩm của giỏ hàng sang 1 đơn hàng để tiến hành đặt hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<bool> AddItemsFromCartToOrder(string OrderId);
    }
}
