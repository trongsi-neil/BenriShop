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
        public async Task<OrderItem> AddOrderItem(OrderItem OrderItem)
        {
            try
            {
                var result = await _context.OrderItems.AddAsync(OrderItem);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrderItem(string OrderId, int ProductId)
        {
            var orderItem = await _context.OrderItems.FindAsync(OrderId, ProductId);
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

        public async Task<OrderItem> GetOrderItem(string OrderId, int ProductId)
        {
            return await _context.OrderItems.FindAsync(OrderId, ProductId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItems(string OrderId)
        {
            return await _context.OrderItems.Where(x => x.OrderId == OrderId).ToListAsync();
        }

        public async Task<OrderItem> UpdateOrderItem(OrderItem OrderItem)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync
                (e => e.OrderId == OrderItem.OrderId && e.ProductId == OrderItem.ProductId);

            if (result != null)
            {
                result.ProductId = OrderItem.ProductId;
                result.OrderId = OrderItem.OrderId;
                result.QuantityInOrder = OrderItem.QuantityInOrder;
                result.Order = OrderItem.Order;
                result.Product = OrderItem.Product;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
