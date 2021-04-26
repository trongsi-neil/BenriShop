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
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<IEnumerable<OrderItem>> GetOrderItems(string orderId);
        /// <summary>
        /// Lấy 1 sản phẩm trong 1 đơn hàng bằng cách truyền vào OrderId và ProductId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<OrderItem> GetOrderItem(string orderId, int productId);
        /// <summary>
        /// Thêm một sản phẩm vào một đơn hằng bằng cách truyền vào 1 đối tượng OrderItem
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public Task<OrderItem> AddOrderItem(OrderItem orderItem);
        /// <summary>
        /// Cập nhật số lượng của một sản phẩm trong một đơn hàng bằng cách truyền vào một đối tượng OrderItem với value khác.
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        public Task<OrderItem> UpdateOrderItem(OrderItem orderItem);
        /// <summary>
        /// Xóa một đối tượng khỏi đơn hàng bằng cách truyền vào OrderId và ProductId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<bool> DeleteOrderItem(string orderId, int productId);
    }
}
