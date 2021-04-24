﻿using BenriShop.ApiRepository.Accounts;
using BenriShop.Models;
using BenriShop.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BenriShop.ApiRepository.Products
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductView>> GetProducts();
        Task<ProductView> GetProduct(int productId);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int productId);
        Task<bool> AddImage(int productId, string imageId, string imageLink);
    }
}
