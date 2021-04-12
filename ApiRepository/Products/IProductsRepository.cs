using BenriShop.ApiRepository.Accounts;
using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BenriShop.ApiRepository.Products
{
    interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int ProductId);
        Task<Product> AddProduct(Product Product);
        Task<Product> UpdateProduct(Product Product);
        Task<bool> DeleteProduct(int ProductId);
    }
}
