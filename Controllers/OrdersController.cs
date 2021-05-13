using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenriShop.Models;
using BenriShop.ApiRepository.Orders;
using BenriShop.ApiRepository.CartItems;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BenriShop.ApiRepository.Accounts;
using BenriShop.Models.ViewModel;
using BenriShop.ApiRepository.Shipping;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShippingRepository _shippingRepository;

        public OrdersController(IOrderRepository orderItemRepository, IShippingRepository shippingRepository)
        {
            this._orderRepository = orderItemRepository;
            this._shippingRepository = shippingRepository;
        }

        /// <summary>
        /// Lấy tất cả đơn hàng theo trạng thái
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET: api/Orders/GetOrders/userName
        [Authorize(Roles = "Mod, Admin")]
        [HttpGet("GetOrdersByStatus/{status}")]
        public async Task<IEnumerable<OrderView>> GetOrdersByStatus(int status)
        {
            //var identity = User.Identity as ClaimsIdentity;
            //if (identity.RoleClaimType == "Customer")
            //{
            //    if (identity.Name != userName)
            //    {
            //        return (IEnumerable<Order>)Conflict("Can't access to diffirent account");
            //    }
            //}

            var orders = await _orderRepository.GetOrdersByStatus(status);
            if (orders != null)
            {
                return orders;
            }
            return (IEnumerable<OrderView>)NotFound("Error of GetOrders");
        }

        /// <summary>
        /// Lấy tất cả đơn hàng của 1 tài khoản bằng cách truyền vào userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET: api/Orders/GetOrders/userName
        [Authorize]
        [HttpGet("GetOrders/{userName}")]
        public async Task<List<OrderView>> GetOrders(string userName)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.RoleClaimType == "Customer")
            {
                if (identity.Name != userName)
                {
                    return (List<OrderView>)(IEnumerable<Order>)Conflict("Can't access to diffirent account");
                }
            }

            var orders = await _orderRepository.GetOrders(userName);
            if (orders != null)
            {
                return orders;
            }
            return (List<OrderView>)(IEnumerable<Order>)NotFound("Error of GetOrders");
        }


        /// <summary>
        /// Lấy 1 đơn hàng bằng cách truyền vào 1 orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        // GET: api/Orders/GetOrders
        [Authorize (Roles = "Admin, Mod")]
        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<OrderView>>> GetOrders()
        {
            var identity = User.Identity as ClaimsIdentity;
            var orders = await _orderRepository.GetOrders();

            if (orders != null)
            {
                return orders;
            }
            return NotFound("Error of GetOrder");
        }


        /// <summary>
        /// Thêm một đơn hàng từ giỏ hàng của người dùng, bằng cách truyền vào userName và payment (true là thanh toán online, false là thành toán tiền mặt khi nhận hàng)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        // POST: api/Orders/AddOrder/userName/true
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Customer")]
        [HttpPost("AddOrder/{userName}")]
        public async Task<ActionResult<Order>> AddOrder(string userName, Shipping shipping)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.Name != userName)
            {
                return Conflict("Can't access to diffirent account");
            }

            Order order = new Order();
            var orders = await _orderRepository.GetOrders(userName);
            orders.ToList();

            order.UserName = userName;

            order.OrderId = Guid.NewGuid().ToString();

            order.Payment = false;

            try
            {
                if(await _orderRepository.AddOrder(order) != null)
                {
                    if(await _orderRepository.AddItemFromCartToOrder(order.OrderId, order.UserName))
                    {
                        shipping.OrderId = order.OrderId;
                        shipping.UserName = order.UserName;
                        shipping.ShippingId = Guid.NewGuid().ToString();
                        await _shippingRepository.CreateShipping(shipping);
                        //CreatedAtAction("AddShipping", "ShippingsController", new { }, shipping);
                        return Ok(order.ToString() + shipping.ToString());
                    }
                    else
                    {
                        await _orderRepository.DeleteOrder(order.OrderId);
                        return BadRequest("Không thể chuyển CartItem sang OrderItem");
                    }
                }else
                {
                    return BadRequest("Không thể thêm Order");
                }
            }catch
            {
                return BadRequest("Error in AddItemsFromCartToOrder or AddOrder");
            }
        }


        /// <summary>
        /// Xóa một đơn hàng bằng cách truyền vào username và orderId
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        // DELETE: api/Orders/DeleteOrder/userName/orderId
        [Authorize]
        [HttpDelete("DeleteOrder/{userName}/{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(string userName, string orderId)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.RoleClaimType == "Customer")
            {
                if (identity.Name != userName)
                {
                    return Conflict("Can't access to diffirent account");
                }
            }

            /*var order = await _orderRepository.GetOrder(orderId);
            if (order == null)
            {
                return NotFound("Not found this order");
            }*/
            try
            {
                if(await _orderRepository.DeleteOrder(orderId))
                {
                    return Ok("Delete order is successful");
                }
                else
                {
                    return BadRequest("Error in DeleteOrder");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error in DeleteOrder");
            }
        }
        [Authorize]
        [HttpPut("UpdateOrder")]
        public async Task<ActionResult<Order>> UpdateOrderStatus(Order order)
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity.RoleClaimType == "Customer")
            {
                if (identity.Name != order.UserName)
                {
                    return Conflict("Can't access to diffirent account");
                }
            }
            try
            {
                await _orderRepository.UpdateOrderStatus(order.OrderId, order.Status);
                return Ok("Update status sucessfully");
            }catch(Exception ex)
            {
                return BadRequest("Exception in UpdateOrder");
            }
            
        }
    }
}
