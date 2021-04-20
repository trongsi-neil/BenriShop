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
        public OrderRepository(BenriShopContext context)
        {
            this._context = context;
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

        public async Task<Account> GetAccount(string accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(e => e.UserName == accountId);
        }
        public async Task<bool> AddItemsFromCartToOrder(string orderId)
        {
            try
            {
                IOrderItemRepository _orderItemRepository = new OrderItemRepository(_context);
                ICartItemRepository _cartItemRepository = new CartItemRepository(_context);
                var order = await _context.Orders.FindAsync(orderId);
                var cartItems = await _cartItemRepository.GetCartItems(order.UserName);
                cartItems = cartItems.ToList();
                foreach(CartItem cartItem in cartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.OrderId;
                    orderItem.ProductId = cartItem.ProductId;
                    orderItem.QuantityInOrder = cartItem.QuantityInCart;
                    orderItem.Order = order;
                    orderItem.Product = cartItem.Product;

                    await _orderItemRepository.AddOrderItem(orderItem);
                    await _cartItemRepository.DeleteCartItem(cartItem.UserName, cartItem.ProductId);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
