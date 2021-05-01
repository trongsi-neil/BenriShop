using BenriShop.ApiRepository.Accounts;
using BenriShop.Models;
using BenriShop.Models.ViewModel;
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
                result.Category = product.Category;
                result.CategoryId = product.CategoryId;
                result.HaveTag = product.HaveTag;
                result.Image = product.Image;
                result.Price = product.Price;
                result.ProductDescription = product.ProductDescription;
                result.ProductId = product.ProductId;
                result.ProductName = product.ProductName;
                result.SizeOfProductHadColor = product.SizeOfProductHadColor;
                result.StorageQuantity = product.StorageQuantity;

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

        public async Task<ProductView> GetProduct(int productId)
        {

            var product = await _context.Products.FirstOrDefaultAsync(e => e.ProductId == productId);

            IQueryable<Image> query = _context.Images;

            if (!string.IsNullOrEmpty(productId.ToString()))
            {
                query = query.Where(e => e.ProductId == productId).
                    Select(i => new Image
                    {
                        Link = i.Link
                    }).AsQueryable();
            }

            var lstImages = _context.Images.Where(x => x.ProductId == productId).ToList();
            List<ImageView> lstImageView = new List<ImageView>();

            foreach (Image item in lstImages)
            {
                var imageView = new ImageView()
                {
                    Link = item.Link
                };
                lstImageView.Add(imageView);
            }

            var lstHaveTags = _context.HaveTags.Where(x => x.ProductId == productId).ToList();
            List<HaveTagView> lstHaveTagView = new List<HaveTagView>();

            foreach (HaveTag item in lstHaveTags)
            {
                var haveTagView = new HaveTagView()
                {
                   TagId = item.TagId
                };
                lstHaveTagView.Add(haveTagView);
            }

            var lstSizeOfProductHadColor = _context.SizeOfProductHadColors.Where(x => x.ProductId == productId).ToList();

            List<SizeOfProductHadColorView> lstSizeOfProductHadColorView = new List<SizeOfProductHadColorView>();

            foreach(SizeOfProductHadColor item in lstSizeOfProductHadColor)
            {
                var sizeOfProductHadColorView = new SizeOfProductHadColorView()
                {
                    SizeId = item.SizeId,
                    ColorId = item.ColorId,
                    QuantityInSizeOfColor = item.QuantityInSizeOfColor
                };
                lstSizeOfProductHadColorView.Add(sizeOfProductHadColorView);
            }

            var productView = new ProductView()
            {
                CategoryId = product.CategoryId,
                Price = product.Price,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                HaveTags = lstHaveTagView,
                Images = lstImageView,
                SizeOfProductHadColors = lstSizeOfProductHadColorView,
                StorageQuantity = product.StorageQuantity
            };

            return productView;
        }

        public async Task<IEnumerable<ProductView>> GetProducts()
        {
            var product = _context.Products.ToList();
            var lst = new List<ProductView>();
            foreach(Product pro in product)
            {
                var temp = await GetProduct(pro.ProductId);
                lst.Add(temp);
            }

            //return await _context.Products.ToListAsync();
            return lst;
        }

        public async Task<bool> AddImage(int productId, string imageId, string imageLink)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }
            Image img = new Image
            {
                //img.Product = product;
                //  img.Imageid = imageId;
                Link = imageLink,
                ProductId = product.ProductId
            };

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
