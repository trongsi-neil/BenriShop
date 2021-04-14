using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.ApiRepository.CartItems
{
    public interface ICartItemRepository
    {
        public Task<IEnumerable<CartItem>> GetCartItems(string UserName);
        public Task<CartItem> GetCartItem(string UserName, int ProductId);
        public Task<CartItem> AddCartItem(CartItem CartItem);
        public Task<CartItem> UpdateCartItem(CartItem CartItem);
        public Task<bool> DeleteCartItem(string UserName, int ProductId);
        public Task<bool> AddItemsFromCartToOrder(string OrderId);
    }
}
