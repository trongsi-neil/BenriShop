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
        private readonly BenriShopContext _contextProduct;
        public ProductsRepository(BenriShopContext context)
        {
            this._contextProduct = context;
        }
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                var result = await _contextProduct.Product.AddAsync(product);
                await _contextProduct.SaveChangesAsync();
                return result.Entity;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {

            var product = await _contextProduct.Product.FindAsync(productId);
            if (product != null)
            {
                try
                {
                    _contextProduct.Product.Remove(product);
                    await _contextProduct.SaveChangesAsync();
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
            var result = await _contextProduct.Product.FindAsync(product.Productid);

            if (result != null)
            {
                result.Categoryid = product.Categoryid;
                result.Productname = product.Productname;
                result.Productdescription = product.Productdescription;
                result.Price = product.Price;
                result.Storagequantity = product.Storagequantity;
                result.Cartitem = product.Cartitem;
                result.HaveTag = product.HaveTag;
                result.Image = product.Image;
                result.Orderitem = product.Orderitem;
                result.Sizeofproducthadcolor = product.Sizeofproducthadcolor;
                try
                {
                    await _contextProduct.SaveChangesAsync();
                }catch (Exception ex)
                {
                    throw ex;
                }

                return result;
            }

            return null;
        }

        public async Task<Product> GetProduct(int ProductId)
        {
            return await _contextProduct.Product.FindAsync(ProductId);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _contextProduct.Product.ToListAsync();
        }
    }
}
