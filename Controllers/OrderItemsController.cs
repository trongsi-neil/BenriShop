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
        /// .Lấy tất cả sản phẩm có trong 1 đơn hàng
        /// </summary>
        /// <returns></returns>
        // GET: api/OrderItems
        [HttpGet("orderId")]
        public async Task<IEnumerable<Models.OrderItem>> GetOrderItems(string orderId)
        {
            var orderItems = _orderItemRepository.GetOrderItems(orderId);
            if (orderItems != null)
            {
                return await orderItems;
            }
            return (IEnumerable<Models.OrderItem>)NotFound("Error of GetOrderItem");
        }
        /// <summary>
        /// Cập nhật số lượng sản phẩm trong đơn hàng.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <param name="orderItem"></param>
        /// <returns></returns>
        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> UpdateOrderItem(string orderId, int productId, Models.OrderItem orderItem)
        {
            if (productId != orderItem.ProductId || orderId != orderItem.OrderId)
            {
                return BadRequest("Wrong of productId or orderId");
            }

            var _product = await _orderItemRepository.GetOrderItem(orderItem.OrderId, orderItem.ProductId);

            try
            {
                await _orderItemRepository.UpdateOrderItem(orderItem);
                return Ok("Update orderItem successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }
        }

        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*[HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderItemExists(orderItem.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderItem", new { id = orderItem.ProductId }, orderItem);
        }*/

        // DELETE: api/OrderItems/5
        /*[HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(e => e.ProductId == id);
        }*/
    }
}
