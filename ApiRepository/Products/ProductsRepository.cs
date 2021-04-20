using BenriShop.ApiRepository.Accounts;
using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BenriShop.ApiRepository.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly BenriShopContext _context;
        public ProductsRepository(BenriShopContext context)
        {
            this._context = context;
        }
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                var result = await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return result.Entity;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {

            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                try
                {
                    _context.Products.Remove(product);
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

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _context.Products.FindAsync(product.ProductId);

            if (result != null)
            {
                result.CategoryId = product.CategoryId;
                result.ProductName = product.ProductName;
                result.ProductDescription = product.ProductDescription;
                result.Price = product.Price;
                result.StorageQuantity = product.StorageQuantity;
                result.CartItems = product.CartItems;
                result.HaveTags = product.HaveTags;
                result.Images = product.Images;
                result.OrderItems = product.OrderItems;
                result.SizeOfProductHadColors = product.SizeOfProductHadColors;
                try
                {
                    await _context.SaveChangesAsync();
                }catch (Exception ex)
                {
                    throw ex;
                }

                return result;
            }

            return null;
        }



        //public List<EmployeeDTO> GetImages(int ProductId)
        //{
        //    var result = (from IMAGE in BenriShop
        //                  where Product.ProductId == ProductId
        //                  select new EmployeeDTO
        //                  {
        //                      FullName = emp.FullName,
        //                      Role = emp.Role,
        //                      Designation = emp.Designation
        //                  }).ToList();

        //    return result;
        //}

        public async Task<Product> GetProduct(int productId)
        {

            var product = await _context.Products.FirstOrDefaultAsync(e => e.ProductId == productId);

            //_context.Image.Where(x => x.Imageid.Contains("select * from [image] where ProductId = "+ProductId+";")).Select(x => new { x., x.Name }).ToList();

            IQueryable<Image> query = _context.Images;

            if (!string.IsNullOrEmpty(productId.ToString()))
            {
                //query = query.Where(e => e.Productid.Contains(ProductId.ToString()));
                query = query.Where(e => e.ProductId == productId).
                    Select(i => new Image
                    {
                        Link = i.Link
                    }).AsQueryable();
            }

            //ICollection<Image> image = (ICollection<Image>)_context.Image.Select(t => t.Productid == ProductId).ToListAsync();

            product.Images = await query.ToListAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> AddImage(int productId, string imageId, string imageLink)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            Image img = new Image();
            //img.Product = product;
            img.Imageid = imageId;
            img.Link = imageLink;
            img.ProductId = product.ProductId;

            try
            {
                _context.Images.Add(img);
                var result = await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
