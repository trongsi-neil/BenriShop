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
            //Lấy số lượng sản phẩm cùng loại còn trong database
            var productQuantity = _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId).QuantityInSizeOfColor; 
            
            if (cartItem.QuantityInCart <= productQuantity)
            {
                try
                {
                    //Trừ đi số lượng vừa được thêm vào giỏ hàng
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

        public async Task<bool> DeleteCartItem(string cartItemId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync
                (e => e.CartItemId == cartItemId);
            //Lấy số lượng sản phẩm cùng loại còn trong database
            var productQuantity = _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId).QuantityInSizeOfColor;
            if (cartItem != null)
            {
                try
                {
                    //cộng lại số lượng mới xóa đi
                    _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId)
                        .QuantityInSizeOfColor = productQuantity + cartItem.QuantityInCart;
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

        public async Task<CartItem> GetCartItem(string cartItemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync
                (e => e.CartItemId == cartItemId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string userName)
        {
            return await _context.CartItems.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync
                (e => e.CartItemId == cartItem.CartItemId);
            //Lấy số lượng sản phẩm cùng loại còn trong database
            var productQuantity = _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId).QuantityInSizeOfColor;

            if (result != null)
            {
                result.ColorId = cartItem.ColorId;
                result.ProductId = cartItem.ProductId;
                //Trừ đi số lượng cũ, cộng lại số lượng mới
                _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId)
                        .QuantityInSizeOfColor = productQuantity + cartItem.QuantityInCart - result.QuantityInCart;
                result.QuantityInCart = cartItem.QuantityInCart;
                result.SizeId = cartItem.SizeId;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
