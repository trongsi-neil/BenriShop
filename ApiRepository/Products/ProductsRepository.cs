using BenriShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Products
{
    public class ProductsRepository : IProductsRepository
    {
        public Task<Product> AddProduct(Product Product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product Product)
        {
            throw new NotImplementedException();
        }
    }
}
