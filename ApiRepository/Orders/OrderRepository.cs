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
        public OrderRepository(BenriShopContext context)
        {
            this._context = context;
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
            var order = _context.Orders.FirstOrDefault(x =>x.OrderId == orderId);

            //var orders = await _context.Orders.Where(e => e.OrderId == orderId).ToListAsync();

            if (order != null)
            {
                try
                {
                    var lstOrderItems = await _context.OrderItems.Where(x => x.OrderId == order.OrderId).ToListAsync();
                    foreach(OrderItem orderItem in lstOrderItems)
                    {
                        _context.OrderItems.Remove(orderItem);
                    }
                    var result =_context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            return false;
        }

        public async Task<OrderView> GetOrder(string orderId)
        {
            var orders = await _context.Orders.Where(e => e.OrderId == orderId).ToListAsync();
            
            List<OrderView> orderViews = new List<OrderView>();

            ShippingView shippingView = new ShippingView();

            foreach (Order item in orders)
            {
                var orderView = new OrderView();
                if (item != null)
                {
                    var lstOrderItem = _context.OrderItems.Where(x => x.OrderId == item.OrderId).ToList();
                    List<OrderItemView> orderItemsView = new List<OrderItemView>();
                    foreach (OrderItem orderItem in lstOrderItem)
                    {
                        OrderItemView orderItemView = new OrderItemView();
                        if (orderItem != null)
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
                        shipView.Note = ship.Note;
                        shipView.Order = ship.Order;
                        shipView.ShipAdress = ship.ShipAdress;
                        shipView.ShipPhoneNumber = ship.ShipPhoneNumber;
                        shipView.ShippingCost = ship.ShippingCost;
                        shipView.ShipFullName = ship.ShipFullName;
                        shipView.ShippingId = ship.ShippingId;
                    }

                    orderView.OrderId = item.OrderId;
                    orderView.UserName = item.UserName;
                    //orderView.OrderDate = item;
                    orderView.Status = item.Status;
                    orderView.Payment = item.Payment;
                    orderView.OrderItems = orderItemsView;
                    orderView.Shipping = shipView;

                }
                orderViews.Add(orderView);
            }

            return orderViews[0];
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
                        shipView.Note = ship.Note;
                        shipView.Order = ship.Order;
                        shipView.ShipAdress = ship.ShipAdress;
                        shipView.ShipPhoneNumber = ship.ShipPhoneNumber;
                        shipView.ShippingCost = ship.ShippingCost;
                        shipView.ShipFullName = ship.ShipFullName;
                        shipView.ShippingId = ship.ShippingId;
                    }

                    orderView.OrderId = item.OrderId;
                    orderView.UserName = item.UserName;
                    //orderView.OrderDate = item;
                    orderView.Status = item.Status;
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
                result.OrderItem = order.OrderItem;
                result.Payment = order.Payment;
                result.Shipping = result.Shipping;
                result.Status = order.Status;
                result.UserName = order.UserName;
                result.UserNameNavigation = order.UserNameNavigation;

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
                    OrderItem orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        QuantityInOrder = item.QuantityInCart,

                        ColorId = item.ColorId,
                        SizeId = item.SizeId,
                        OrderItemId = item.CartItemId,
                        UserName =item.UserName,
                       
                        //Order = order
                    };


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
