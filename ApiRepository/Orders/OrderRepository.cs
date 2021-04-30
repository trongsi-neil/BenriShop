using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;
using BenriShop.ApiRepository.Orders;
using Microsoft.EntityFrameworkCore;
using BenriShop.ApiRepository.OrderItems;
using BenriShop.ApiRepository.CartItems;
using BenriShop.Models.ViewModel;

namespace BenriShop.ApiRepository.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BenriShopContext _context;
        private readonly BenriShopContext benriShopContext;
        public OrderRepository(BenriShopContext context)
        {
            this._context = context;
            this.benriShopContext = context;
        }
        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                var result = await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteOrder(string orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                try
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task<Order> GetOrder(string orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders(string userName)
        {
            return await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
        }


        public async Task<IEnumerable<OrderView>> GetOrdersByStatus(int status)
        {
            var orders = await _context.Orders.Where(x => x.Status == status).ToListAsync();
            List<OrderView> orderViews = new List<OrderView>();

            ShippingView shippingView = new ShippingView();

            foreach (Order item in orders)
            {
                var orderView = new OrderView(); 
                if(item != null)
                {
                    var lstOrderItem = _context.OrderItems.Where(x => x.OrderId == item.OrderId).ToList();
                    List<OrderItemView> orderItemsView = new List<OrderItemView>();
                    foreach(OrderItem orderItem in lstOrderItem)
                    {
                        OrderItemView orderItemView = new OrderItemView();
                        if(orderItem != null)
                        {
                            orderItemView.OrderId = orderItem.OrderId;
                            orderItemView.OrderItemId = orderItem.OrderItemId;
                            orderItemView.ProductId = orderItem.ProductId;
                            orderItemView.ColorId = orderItem.ColorId;
                            orderItemView.SizeId = orderItem.SizeId;
                            orderItemView.QuantityInOrder = orderItem.QuantityInOrder;
                        }
                        orderItemsView.Add(orderItemView);
                    }


                    var ship = _context.Shippings.FirstOrDefault(x => x.OrderId == item.OrderId);
                    ShippingView shipView = new ShippingView();
                    if (ship != null)
                    {
                        //OrderView order = new OrderView();
                        //order.OrderId = ship.OrderId;
                        //order.OrderItems = 

                        //shipView.Note = ship.Note;
                        //shipView.Order = ship.Order;
                        //shipView.ShipAdress = ship.ShipAdress;
                        //shipView.Status = ship.Status;
                    }
                       
                    


                    orderView.OrderId = item.OrderId;
                    orderView.UserName = item.UserName;
                    //orderView.OrderDate = item.OrderDate;
                    //orderView.Status = item.Status;
                    orderView.Payment = item.Payment;
                    orderView.OrderItems = orderItemsView;
                    orderView.Shipping = shipView;

                }
                orderViews.Add(orderView);
            }

            return orderViews;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == order.OrderId);

            if (result != null)
            {
                result.OrderId = order.OrderId;
                result.UserName = order.UserName;
                result.Payment = order.Payment;
                //result.Account = result.Account;
                //result.OrderItems = order.OrderItems;
                //result.Shippings = order.Shippings;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<bool> AddItemFromCartToOrder(string orderId, string userName)
        {
            try
            {
          
                var order =  _context.Orders.FirstOrDefault( x => x.OrderId == orderId);
                var cartItems = _context.CartItems.Where(x => x.UserName == userName).ToList();
                if (cartItems.Count == 0) return false;
                foreach (CartItem item in cartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = order.OrderId;
                    orderItem.ProductId = item.ProductId;
                    orderItem.QuantityInOrder = item.QuantityInCart;

                    orderItem.ColorId = item.ColorId;
                    orderItem.SizeId = item.SizeId;
                    orderItem.Order = order;
                 

                    _context.OrderItems.Add(orderItem);
                    _context.CartItems.Remove(item);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
