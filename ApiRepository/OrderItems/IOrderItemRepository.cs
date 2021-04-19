using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.ApiRepository.OrderItems
{
    public interface IOrderItemRepository
    {
        /// <summary>
        /// Lấy tất cả sản phẩm trong 1 đơn hàng bằng cách truyền vào OrderId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public Task<IEnumerable<OrderItem>> GetOrderItems(string OrderId);
        /// <summary>
        /// Lấy 1 sản phẩm trong 1 đơn hàng bằng cách truyền vào OrderId và ProductId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Task<OrderItem> GetOrderItem(string OrderId, int ProductId);
        /// <summary>
        /// Thêm một sản phẩm vào một đơn hằng bằng cách truyền vào 1 đối tượng OrderItem
        /// </summary>
        /// <param name="OrderItem"></param>
        /// <returns></returns>
        public Task<OrderItem> AddOrderItem(OrderItem OrderItem);
        /// <summary>
        /// Cập nhật số lượng của một sản phẩm trong một đơn hàng bằng cách truyền vào một đối tượng OrderItem với value khác.
        /// </summary>
        /// <param name="OrderItem"></param>
        /// <returns></returns>
        public Task<OrderItem> UpdateOrderItem(OrderItem OrderItem);
        /// <summary>
        /// Xóa một đối tượng khỏi đơn hàng bằng cách truyền vào OrderId và ProductId
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public Task<bool> DeleteOrderItem(string OrderId, int ProductId);
    }
}
