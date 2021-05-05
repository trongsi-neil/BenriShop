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
using BenriShop.Models.ViewModel;

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

            public List<IFormFile> files { get; set; }
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

        // POST: api/Products/AddProduct
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Thêm sản phẩm vào cửa hàng
        /// </summary>
        /// <param product="Product"></param>
        /// <returns></returns>
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<ActionResult<AddProductView>> AddProduct(AddProductView addProductView)
        {
            try
            {
                var product = await _productRepository.AddProduct(addProductView);
                return Ok("Add product sucessfull");
            }catch
            {
                return BadRequest("Error when call _productRepository.AddProduct(product)");
            }
        }
        /// <summary>
        /// Thêm tag cho 1 sản phẩm bằng cách truyền vào 1 đối tượng HaveTag
        /// </summary>
        /// <param name="haveTag"></param>
        /// <returns></returns>
        [HttpPost("AddTag")]
        public async Task<ActionResult<Product>> AddTag(HaveTag haveTag)
        {
            try
            {
                if (await _productRepository.AddTag(haveTag.ProductId, haveTag.TagId))
                {
                    return Ok("Add tag is successfully!");
                }else
                {
                    return BadRequest("Error in AddTag!");
                }
            }
            catch
            {
                return BadRequest("Error in AddTag!");
            }
        }

        //POST: api/Product/AddSizeAndColor
        [HttpPost("AddSizeAndColor")]
        public async Task<ActionResult> AddSizeAndColor(SizeOfProductHadColor sizeOfProductHadColor)
        {
            try
            {
                if (await _productRepository.AddSizeAndColor(sizeOfProductHadColor))
                {
                    return Ok("Add size and color is successfully!");
                }else
                {
                    return BadRequest("Error in AddSizeAndColor!");
                }
                
            }
            catch
            {
                return BadRequest("Error in AddSizeAndColor!");
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
        // GET: api/Products/GetProducts
        [HttpGet("GetProducts")]
        public async Task<IEnumerable<ProductView>> GetProducts()
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
        public async Task<ActionResult<ProductView>> GetProduct(int id)
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
        //POST: api/Product/UploadImage
        /// <summary>
        /// Nhận hình tải lên và lưu vào thư mục images
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        //[Route("api/[controller]/uploadimage")]
        public async Task<ActionResult> UploadImage([FromForm] FileUpload objFile)
        {
            foreach(IFormFile file in objFile.files)
            {
                if (file.Length > 0)
                {
                    objFile.Id = objFile.ProductId + "_" + file.FileName;
                    //objFile.Id = new Guid().ToString();
                    //                objFile.Link = _environment.WebRootPath + "\\images\\" + objFile.Id;
                    objFile.Link = "\\images\\" + objFile.Id;
                    if (objFile.Id.Length > 200)
                    {
                        return BadRequest("File's name is longer 200 character");
                    }
                    try
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\images\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\images\\");
                        }
                        using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + objFile.Link))
                        {
                            file.CopyTo(fileStream);
                            fileStream.Flush();
                            await _productRepository.AddImage(objFile.ProductId, objFile.Id, objFile.Link);
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
            return Ok("Upload image is successful");
            
        }
        
        #endregion
    }
}
