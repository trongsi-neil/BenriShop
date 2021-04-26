using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.OrderItems
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly BenriShopContext _context;
        public OrderItemRepository(BenriShopContext context)
        {
            this._context = context;
        }
        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            try
            {
                var result = await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrderItem(string orderId, int productId)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync
                (e => e.OrderId == orderId && e.ProductId == productId);
            if (orderItem != null)
            {
                try
                {
                    _context.OrderItems.Remove(orderItem);
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

        public async Task<OrderItem> GetOrderItem(string orderId, int productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync
                (e => e.OrderId == orderId && e.ProductId == productId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems(string orderId)
        {
            return await _context.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem orderItem)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync
                (e => e.OrderId == orderItem.OrderId && e.ProductId == orderItem.ProductId);

            if (result != null)
            {
                result.ProductId = orderItem.ProductId;
                result.OrderId = orderItem.OrderId;
                result.QuantityInOrder = orderItem.QuantityInOrder;
                result.Order = orderItem.Order;
                result.Product = orderItem.Product;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
