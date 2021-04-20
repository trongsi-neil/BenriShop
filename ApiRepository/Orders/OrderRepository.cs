using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;
using BenriShop.ApiRepository.Orders;
using Microsoft.EntityFrameworkCore;
using BenriShop.ApiRepository.OrderItems;
using BenriShop.ApiRepository.CartItems;

namespace BenriShop.ApiRepository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BenriShopContext _context;
        private readonly BenriShopContext benriShopContext;
        public OrderRepository(BenriShopContext context)
        {
            this._context = context;
            this.benriShopContext = context;
        }
        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                var result = await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrder(string orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                try
                {
                    _context.Orders.Remove(order);
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

        public async Task<Order> GetOrder(string orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(e => e.UserName == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(string userName)
        {
            return await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == order.OrderId);

            if (result != null)
            {
                result.OrderId = order.OrderId;
                result.UserName = order.UserName;
                result.Payment = order.Payment;
                result.Account = result.Account;
                result.OrderItems = order.OrderItems;
                result.Shippings = order.Shippings;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(string userName)
        {
            return await _context.CartItems.Where(x => x.UserName == userName).ToListAsync();
        }
        public async Task<bool> AddItemFromCartToOrder(string orderId, CartItem cartItem)
        {
            IOrderItemRepository _orderItemRepository = new OrderItemRepository(_context);
            ICartItemRepository _cartItemRepository = new CartItemRepository(_context);

            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = order.OrderId;
                orderItem.ProductId = cartItem.ProductId;
                orderItem.QuantityInOrder = cartItem.QuantityInCart;
                orderItem.Order = order;
                orderItem.Product = cartItem.Product;

                await _orderItemRepository.AddOrderItem(orderItem);
                await _cartItemRepository.DeleteCartItem(order.UserName, cartItem.ProductId);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            /*try
            {
                IOrderItemRepository _orderItemRepository = new OrderItemRepository(_context);
                var order = await _context.Orders.FindAsync(orderId);
                var cartItems = await benriShopContext.CartItems.Where(x => x.UserName == order.UserName).ToListAsync();

                foreach (CartItem cartItem in cartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.OrderId;
                    orderItem.ProductId = cartItem.ProductId;
                    orderItem.QuantityInOrder = cartItem.QuantityInCart;
                    orderItem.Order = order;
                    orderItem.Product = cartItem.Product;

                    await _orderItemRepository.AddOrderItem(orderItem);
                    _context.CartItems.Remove(cartItem);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }*/

        }
    }
}
