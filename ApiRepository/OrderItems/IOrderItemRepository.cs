using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.ApiRepository.OrderItems
{
    interface IOrderItemRepository
    {
        public Task<IEnumerable<OrderItem>> GetOrderItems(string OrderId);
        public Task<OrderItem> GetOrderItem(string OrderId, int ProductId);
        public Task<OrderItem> AddOrderItem(OrderItem OrderItem);
        public Task<OrderItem> UpdateOrderItem(OrderItem OrderItem);
        public Task<bool> DeleteOrderItem(string OrderId, int ProductId);
    }
}
