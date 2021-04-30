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
        public async Task<CartItem> AddCartItem(CartItem cartItem)
        {

            //var product = (from pro in _context.Products
            //                        join sizeColor in _context.SizeOfProductHadColors on pro.ProductId equals sizeColor.ProductId
            //                        where pro.ProductId == cartItem.ProductId && sizeColor.SizeId == Size && sizeColor.ColorId == Color
            //                        select sizeColor.QuantityInSizeOfColor, pro.ProductId).ToList();

            var productQuantity = _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId).QuantityInSizeOfColor; 

            
            if (cartItem.QuantityInCart <= productQuantity)
            {
                try
                {
                    _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId)
                        .QuantityInSizeOfColor = productQuantity - cartItem.QuantityInCart;

                    _context.CartItems.Add(cartItem);
                    
                    await _context.SaveChangesAsync();

                    return cartItem;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return null;
            }


        }

        /*public async Task<bool> AddItemsFromCartToOrder(string orderId)
        {
            try
            {
                IOrderItemRepository _orderItemRepository = new OrderItemRepository(_context);
                var order = await _context.Orders.FindAsync(orderId);
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
                    account.CartItems.RemoveAt(i);
                }
                await _context.SaveChangesAsync();
                return true;
            }catch
            {
                return false;
            }
        }*/

        public async Task<bool> DeleteCartItem(string userName, int productId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == userName && e.ProductId == productId);
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

        public async Task<CartItem> GetCartItem(string userName, int productId)
        {
            return await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == userName && e.ProductId == productId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string userName)
        {
            return await _context.CartItems.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync
                (e => e.UserName == cartItem.UserName && e.ProductId == cartItem.ProductId);

            if (result != null)
            {
                result.ProductId = cartItem.ProductId;
                result.UserName = cartItem.UserName;
                result.QuantityInCart = cartItem.QuantityInCart;
               // result.Product = cartItem.Product;
               // result.Account = cartItem.Account;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
