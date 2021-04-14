using BenriShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Orders
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrders(string userName);
        public Task<Order> GetOrder(string OrderId);
        public Task<Order> AddOrder(Order Order);
        public Task<Order> UpdateOrder(Order Order);
        public Task<bool> DeleteOrder(string OrderId);
    }
}
