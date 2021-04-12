using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenriShop.Models;
using Microsoft.AspNetCore.Authorization;
using BenriShop.ApiRepository.Products;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productRepository;

        public ProductsController(IProductsRepository productRepository)
        {
            this._productRepository = productRepository;
        }


        #region Admin
        [Authorize(Roles = "Admin")]
        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Productid)
            {
                return BadRequest();
            }
            var _product = await _productRepository.GetProduct(product.Productid);
            
            if (_product == null)
            {
                return NotFound();
            }

            try
            {
                await _productRepository.UpdateProduct(product);
                return Ok("Update product successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NoContent();
            }

        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Thêm tài khoản nhân viên
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {


            return CreatedAtAction("GetProduct", new { id = product.Productid }, product);
        }


        // DELETE: api/Products/5
        //[HttpDelete("{id}")]
        /*public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }*/
        #endregion

        #region Users

       // GET: api/Products
       [HttpGet("GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

       // GET: api/Products/5
       [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        #endregion

        #region Method

        /*private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Productid == id);
        }*/
        #endregion

    }
}
