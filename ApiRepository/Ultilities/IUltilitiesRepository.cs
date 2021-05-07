using BenriShop.Models;
using BenriShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Ultilities
{
    public interface IUltilitiesRepository
    {
        /// <summary>
        /// Lấy tất cả đối tượng Size được lưu trong database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Size>> GetSizes();

        /// <summary>
        /// Lấy tất cả đối tượng Size được lưu trong database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Tag>> GetTags();

        /// <summary>
        /// Lấy tất cả đối tượng Color được lưu trong database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Color>> GetColors();

        /// <summary>
        /// Lấy tất cả đối tượng Categogy được lưu trong database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategories();

    }
}
