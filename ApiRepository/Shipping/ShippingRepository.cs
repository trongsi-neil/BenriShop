using BenriShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Shipping
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly BenriShopContext _context;
        public ShippingRepository(BenriShopContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> CreateShipping(Models.Shipping shipping)
        {
            try
            {
                var result = await _context.Shippings.AddAsync(shipping);
                await _context.SaveChangesAsync();
                return (IActionResult)result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<bool> DeleteShipping(string shippingId)
        {
            var shipping = await _context.Shippings.FindAsync(shippingId);
            if (shipping != null)
            {
                try
                {
                    _context.Shippings.Remove(shipping);
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

        public async Task<Models.Shipping> GetShipping(string orderId)
        {
            return await _context.Shippings.FirstOrDefaultAsync(e => e.OrderId == orderId);
        }
    }
}
