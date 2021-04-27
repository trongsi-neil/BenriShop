using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.ApiRepository.Shipping
{
    public interface IShippingRepository
    {
        /// <summary>
        /// Tạo ra 1 đối tượng Shipping trong database bằng cách truyền vào đối tượng đó.
        /// </summary>
        /// <param name="shipping"></param>
        /// <returns></returns>
        public Task<IActionResult> CreateShipping(BenriShop.Models.Shipping shipping);
        /// <summary>
        /// Xóa 1 Shipping trong database bằng cách truyền vào 1 shippingId của đối tượng muốn xóa
        /// </summary>
        /// <param name="shippingId"></param>
        /// <returns></returns>
        public Task<bool> DeleteShipping(string shippingId);
        /// <summary>
        /// Lấy 1 đối tượng Shipping trong database bằng cách truyền vào 1 shippingId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<Models.Shipping> GetShipping(string shippingId);
    }
}
