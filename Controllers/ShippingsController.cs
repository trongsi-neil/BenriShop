using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenriShop.ApiRepository.Shipping;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BenriShop.Models;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingsController : ControllerBase
    {
        private readonly IShippingRepository _shippingRepository;

        public ShippingsController(IShippingRepository shippingRepository)
        {
            this._shippingRepository = shippingRepository;
        }
        [Authorize(Roles = "Admin, Mod")]
        // GET: api/Shippings/GetShipping/5
        [HttpGet("GetShipping/{shippingId}")]
        public async Task<ActionResult<Shipping>> GetShipping(string shippingId)
        {
            var order = await _shippingRepository.GetShipping(shippingId);

            if (order != null)
            {
                return order;
            }
            return NotFound("Error of GetShipping");
        }



        // POST: api/Shippings/AddShipping
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin, Mod")]
        [HttpPost("AddShipping")]
        public async Task<ActionResult<Shipping>> AddShipping(Shipping shipping)
        {
            try
            {
                shipping.ShippingId = Guid.NewGuid().ToString();
                await _shippingRepository.CreateShipping(shipping);
                return Ok("Add shipping successfully!");
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Error when CreateShipping");
            }
            
        }

        // DELETE: api/Shippings/DeleteShipping/5
        [Authorize(Roles = "Admin, Mod")]
        [HttpDelete("DeleteShipping/{shippingId}")]
        public async Task<ActionResult<Shipping>> DeleteShipping(string shippingId)
        {
            var order = _shippingRepository.GetShipping(shippingId);
            if (order == null)
            {
                return NotFound("Not found this shipping");
            }
            try
            {
                await _shippingRepository.DeleteShipping(shippingId);
                return Ok("Delete order is successful");
            }
            catch
            {
                return BadRequest("Error in DeleteShipping");
            }
        }
    }
}
