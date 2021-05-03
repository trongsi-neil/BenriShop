using BenriShop.ApiRepository.OrderItems;
using BenriShop.ApiRepository.Products;
using BenriShop.Models;
using BenriShop.Models.ViewModel;
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
            //Lấy số lượng sản phẩm cùng loại còn trong database
            var productDetailQuantity = _context.SizeOfProductHadColors.FirstOrDefault(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId).QuantityInSizeOfColor;
            /*//Lấy số lượng tổng của sản phẩm còn trong database
            var productQuantity = _context.Products.FirstOrDefault(x => x.ProductId == cartItem.ProductId).StorageQuantity;*/
            //Kiểm tra sản phẩm đã được thêm vào giỏ hàng trước đó hay chưa, nếu đã được thêm thì tăng số lượng lên 1
            UpdateQuantityAsync(cartItem.ProductId, cartItem.SizeId, cartItem.ColorId, 0);
            var cartItemIsExist = _context.CartItems.FirstOrDefault(x => x.UserName == cartItem.UserName && x.ProductId == cartItem.ProductId && x.SizeId == cartItem.SizeId && x.ColorId == cartItem.ColorId);
            if (cartItemIsExist != null)
            {
                if (cartItemIsExist.QuantityInCart <= productDetailQuantity)
                {
                    cartItemIsExist.QuantityInCart += cartItem.QuantityInCart;
                    /*//Trừ đi số lượng vừa được thêm vào giỏ hàng
                    _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId)
                        .QuantityInSizeOfColor = productDetailQuantity - cartItem.QuantityInCart;
                    _context.Products.First(x => x.ProductId == cartItem.ProductId).StorageQuantity = productQuantity - cartItem.QuantityInCart;*/
                    UpdateQuantityAsync(cartItem.ProductId, cartItem.SizeId, cartItem.ColorId, 0 - cartItem.QuantityInCart);
                    await _context.SaveChangesAsync();
                    return cartItemIsExist;
                }
                else
                {
                    return null;
                }
            }
            if (cartItem.QuantityInCart <= productDetailQuantity)
            {
                try
                {
                    /*//Trừ đi số lượng vừa được thêm vào giỏ hàng
                    _context.SizeOfProductHadColors.First(x => x.ProductId == cartItem.ProductId && x.ColorId == cartItem.ColorId && x.SizeId == cartItem.SizeId)
                        .QuantityInSizeOfColor = productDetailQuantity - cartItem.QuantityInCart;
                    _context.Products.First(x => x.ProductId == cartItem.ProductId).StorageQuantity = productQuantity - cartItem.QuantityInCart;*/
                    UpdateQuantityAsync(cartItem.ProductId, cartItem.SizeId, cartItem.ColorId, 0 - cartItem.QuantityInCart);
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
            
            if (cartItem != null)
            {
                try
                {
                    UpdateQuantityAsync(cartItem.ProductId, cartItem.SizeId, cartItem.ColorId, cartItem.QuantityInCart);
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

        public async Task<IEnumerable<CartItemView>> GetCartItems(string userName)
        {
            List<CartItemView> cartItemViews = new List<CartItemView>();
            List<CartItem> cartItems = await  _context.CartItems.Where(x => x.UserName == userName).ToListAsync();
            List<Product> products = new List<Product>();
            foreach (CartItem cartItem in cartItems)
            {
                ProductsRepository productsRepository = new ProductsRepository(_context);
                CartItemView cartItemView = new CartItemView();

                cartItemView.ProductView = await productsRepository.GetProduct(cartItem.ProductId);
                cartItemView.ProductId = cartItem.ProductId;
                cartItemView.QuantityInCart = cartItem.QuantityInCart;
                cartItemView.SizeId = cartItem.SizeId;
                cartItemView.UserName = cartItem.UserName;
                cartItemView.ColorId = cartItem.ColorId;
                cartItemView.CartItemId = cartItem.CartItemId;

                cartItemViews.Add(cartItemView);
            }
            return cartItemViews;
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync
                (e => e.CartItemId == cartItem.CartItemId);
            int quantityUpdate = result.QuantityInCart - cartItem.QuantityInCart;
            if (result != null)
            {
                result.ColorId = cartItem.ColorId;
                result.ProductId = cartItem.ProductId;
                UpdateQuantityAsync(cartItem.ProductId, cartItem.SizeId, cartItem.ColorId, quantityUpdate);
                result.QuantityInCart = cartItem.QuantityInCart;
                result.SizeId = cartItem.SizeId;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
        private void UpdateQuantityAsync(int productId, string sizeId, string colorId, int quantityInCart)
        {
            //Lấy số lượng sản phẩm cùng loại còn trong database
            var productDetailQuantity = _context.SizeOfProductHadColors.First(x => x.ProductId == productId && x.ColorId == colorId && x.SizeId == sizeId).QuantityInSizeOfColor;
            //Lấy số lượng tổng của sản phẩm còn trong database
            var productQuantity = _context.Products.First(x => x.ProductId == productId).StorageQuantity;
            //cập nhật lại số lượng
            _context.SizeOfProductHadColors.First(x => x.ProductId == productId && x.ColorId == colorId && x.SizeId == sizeId)
                .QuantityInSizeOfColor = productDetailQuantity + quantityInCart;
            _context.Products.First(x => x.ProductId == productId).StorageQuantity = productQuantity + quantityInCart;
        }
    }
}
