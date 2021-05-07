using BenriShop.ApiRepository.Products;
using BenriShop.ApiRepository.Ultilities;
using BenriShop.Models;
using BenriShop.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UltilitiesController : ControllerBase
    {
        private readonly IUltilitiesRepository _ultilitiesRepository;


        public UltilitiesController(IUltilitiesRepository ultilitiesRepository)
        {
            this._ultilitiesRepository = ultilitiesRepository;
        }

        /// <summary>
        /// Lấy tất cả Size trong database
        /// </summary>
        /// <returns></returns>
        // GET: api/Products/GetProducts
        [HttpGet("GetSizes")]
        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await _ultilitiesRepository.GetSizes();
        }

        /// <summary>
        /// Lấy tất cả Tag trong database
        /// </summary>
        /// <returns></returns>
        // GET: api/Products/GetProducts
        [HttpGet("GetTags")]
        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _ultilitiesRepository.GetTags();
        }
        /// <summary>
        /// Lấy tất cả sản phẩm trong database
        /// </summary>
        /// <returns></returns>
        // GET: api/Products/GetProducts
        [HttpGet("GetColors")]
        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _ultilitiesRepository.GetColors();
        }

        /// <summary>
        /// Lấy tất cả Category trong database
        /// </summary>
        /// <returns></returns>
        // GET: api/Products/GetProducts
        [HttpGet("GetCategories")]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _ultilitiesRepository.GetCategories();
        }

    }

}
