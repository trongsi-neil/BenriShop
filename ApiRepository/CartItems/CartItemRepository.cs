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
                result.CartItemId = cartItem.CartItemId;
                result.ColorId = cartItem.ColorId;
                result.ProductId = cartItem.ProductId;
                result.QuantityInCart = cartItem.QuantityInCart;
                result.SizeId = cartItem.SizeId;
                result.SizeOfProductHadColor = cartItem.SizeOfProductHadColor;
                result.UserName = cartItem.UserName;
                result.UserNameNavigation = cartItem.UserNameNavigation;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
