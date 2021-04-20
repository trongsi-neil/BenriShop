using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenriShop.Models;
using BenriShop.ApiRepository.OrderItems;
using Stripe;
using Microsoft.AspNetCore.Authorization;
using BenriShop.ApiRepository.Accounts;
using System.Security.Claims;
using BenriShop.ApiRepository.Orders;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository)
        {
            this._orderItemRepository = orderItemRepository;
        }
        /// <summary>
        /// Lấy tất cả sản phẩm có trong 1 đơn hàng
        /// </summary>
        /// <returns></returns>
        // GET: api/OrderItems/GetOrderItems/userName/orderId
        [Authorize]
        [HttpGet("GetOrderItems/{userName}/{orderId}")]
        public async Task<IEnumerable<Models.OrderItem>> GetOrderItems(string userName, string orderId)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.RoleClaimType == "Customer")
            {
                if (identity.Name != userName)
                {
                    return (IEnumerable<Models.OrderItem>)Conflict("Can't access to diffirent account");
                }
            }
            
            var orderItems = await _orderItemRepository.GetOrderItems(orderId);
            if (orderItems != null)
            {
                return orderItems;
            }
            return (IEnumerable<Models.OrderItem>)NotFound("Error of GetOrderItem");
        }
    }
}
