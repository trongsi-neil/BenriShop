using BenriShop.Models;
using BenriShop.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Ultilities
{
    public class UltilitiesRepository : IUltilitiesRepository
    {
        private readonly BenriShopContext _context;

        public UltilitiesRepository(BenriShopContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categorys.ToListAsync();
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

    }
}
