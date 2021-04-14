using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;
using BenriShop.ApiRepository.Orders;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteOrder(string OrderId)
        {
            var order = await _context.Orders.FindAsync(OrderId);
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

        public async Task<Order> GetOrder(string OrderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(e => e.UserName == OrderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(string userName)
        {
            return await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
        }

        public async Task<Order> UpdateOrder(Order Order)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == Order.OrderId);

            if (result != null)
            {
                result.OrderId = Order.OrderId;
                result.UserName = Order.UserName;
                result.Payment = Order.Payment;
                result.Account = result.Account;
                result.OrderItems = Order.OrderItems;
                result.Shippings = Order.Shippings;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
