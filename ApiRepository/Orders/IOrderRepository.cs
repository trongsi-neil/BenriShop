using BenriShop.Models;
using BenriShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Orders
{
    public interface IOrderRepository
    {

        /// <summary>
        /// Lấy tất cả đơn hàng của theo status
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<IEnumerable<OrderView>> GetOrdersByStatus(int status);


        /// <summary>
        /// Lấy tất cả đơn hàng của 1 tài khoản bằng cách truyền vào username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<List<OrderView>> GetOrders(string userName);
        /// <summary>
        /// Lấy tất cả đơn hàng của tất cả tài khoản trong hệ thống.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<List<OrderView>> GetOrders();
        /// <summary>
        /// Thêm 1 đơn hàng bằng cách truyền vào 1 đối tượng Order với đầy đủ value
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public Task<Order> AddOrder(Order order);
        /// <summary>
        /// Cập nhật thông tin đơn hàng bằng cách truyền vào 1 đối tượng Order với value mới
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public Task<Order> UpdateOrder(Order order);
        /// <summary>
        /// Xóa 1 đơn hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<bool> DeleteOrder(string orderId);

        public Task<bool> AddItemFromCartToOrder(string orderId, string userName);
    }
}
