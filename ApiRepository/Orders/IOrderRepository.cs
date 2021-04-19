using BenriShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Orders
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Lấy tất cả đơn hàng của 1 tài khoản bằng cách truyền vào username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<IEnumerable<Order>> GetOrders(string userName);
        /// <summary>
        /// Lấy 1 đơn hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<Order> GetOrder(string OrderId);
        /// <summary>
        /// Thêm 1 đơn hàng bằng cách truyền vào 1 đối tượng Order với đầy đủ value
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public Task<Order> AddOrder(Order Order);
        /// <summary>
        /// Cập nhật thông tin đơn hàng bằng cách truyền vào 1 đối tượng Order với value mới
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public Task<Order> UpdateOrder(Order Order);
        /// <summary>
        /// Xóa 1 đơn hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<bool> DeleteOrder(string OrderId);
    }
}
