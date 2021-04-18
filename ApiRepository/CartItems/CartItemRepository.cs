using BenriShop.ApiRepository.OrderItems;
using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.CartItems
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly BenriShopContext _context;
        public CartItemRepository(BenriShopContext context)
        {
            this._context = context;
        }
        public async Task<CartItem> AddCartItem(CartItem CartItem)
        {
            try
            {
                var result = await _context.CartItems.AddAsync(CartItem);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddItemsFromCartToOrder(string OrderId)
        {
            try
            {
                IOrderItemRepository _orderItemRepository = new OrderItemRepository(_context);
                var order = await _context.Orders.FindAsync(OrderId);
                var account = await _context.Accounts.FirstOrDefaultAsync(e => e.UserName == order.UserName);
                for (int i = 0; i < account.CartItems.Count; i++)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.OrderId;
                    orderItem.ProductId = account.CartItems[i].ProductId;
                    orderItem.QuantityInOrder = account.CartItems[i].QuantityInCart;
                    orderItem.Order = order;
                    orderItem.Product = account.CartItems[i].Product;

                    await _orderItemRepository.AddOrderItem(orderItem);
                    await _context.SaveChangesAsync();
                }
                return true;
            }catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCartItem(string UserName, int ProductId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == UserName && e.ProductId == ProductId);
            if (cartItem != null)
            {
                try
                {
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task<CartItem> GetCartItem(string UserName, int ProductId)
        {
            return await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == UserName && e.ProductId == ProductId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string userName)
        {
            return await _context.CartItems.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<CartItem> UpdateCartItem(CartItem CartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == CartItem.UserName && e.ProductId == CartItem.ProductId);

            if (result != null)
            {
                result.ProductId = CartItem.ProductId;
                result.UserName = CartItem.UserName;
                result.QuantityInCart = CartItem.QuantityInCart;
                result.Product = CartItem.Product;
                result.Account = CartItem.Account;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
