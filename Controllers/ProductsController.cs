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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productRepository;
        public static IWebHostEnvironment _environment;

        public ProductsController(IProductsRepository productRepository, IWebHostEnvironment environment)
        {
            this._productRepository = productRepository;

            _environment = environment; 
        }

        public class FileUpload
        {
            public int ProductId { get; set; }

            public string Id { get; set; }

            public string Link { get; set; }

            public IFormFile files { get; set; }
        }

        #region Admin
        [Authorize(Roles = "Admin, Mod")]
        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Sửa thông tin sản phẩm
        /// </summary>
        /// <param id="ProductId"></param>
        /// <returns></returns>
        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Wrong of ProductId");
            }
            var _product = await _productRepository.GetProduct(product.ProductId);
            
            if (_product == null)
            {
                return NotFound("Not found this product with this productId");
            }

            try
            {
                await _productRepository.UpdateProduct(product);
                return Ok("Update product successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Error when call _productRepository.UpdateProduct(product)");
            }

        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Thêm sản phẩm vào cửa hàng
        /// </summary>
        /// <param product="Product"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                await _productRepository.AddProduct(product);
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }catch
            {
                return BadRequest("Error when call _productRepository.AddProduct(product)");
            }
        }

        /// <summary>
        /// Xóa sản phẩm bằng cách truyền vào productId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Products/5
        [HttpDelete("{productId}")]
        public async Task<ActionResult<Product>> DeleteProduct(int productId)
        {
            if (await _productRepository.DeleteProduct(productId))
            {
                return Ok("Delete successfully!");
            }
            else
            {
                return BadRequest("Error when call _productRepository.DeleteProduct(productId)");
            }
        }
        #endregion

        #region Users
        /// <summary>
        /// Lấy tất cả sản phẩm trong database
        /// </summary>
        /// <returns></returns>
        // GET: api/Products
        [HttpGet("GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }
        /// <summary>
        /// Lấy một sản phẩm bằng cách truyền vào productId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Nhận hình tải lên và lưu vào thư mục images
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        //[Route("api/[controller]/uploadimage")]
        public async Task<ActionResult> UploadImage([FromForm] FileUpload objFile)
        {
            if (objFile.files.Length > 0)
            {
                objFile.Id = objFile.ProductId + "_" + objFile.files.FileName;
                objFile.Link = _environment.WebRootPath + "\\images\\" + objFile.Id;
                if (objFile.Id.Length > 20)
                {
                    return BadRequest("File's name is longer 20 character");
                }
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\images\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(objFile.Link))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        await _productRepository.AddImage(objFile.ProductId, objFile.Id, objFile.Link);
                        return Ok("Upload image is successful");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return BadRequest("Fail in upload image!");
            }
        }

        #endregion

    }
}
