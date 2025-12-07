using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class ProductCategoryRepo : GenericRepository<ProductCategory>, IProductCategory
    {
        public ProductCategoryRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductCategory>> GetChildrenAsync(int parentId)
        {
            return await _context.ProductCategories
                .Where(c => c.ParentCategoryId == parentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetParentCategoriesAsync()
        {
            return await _context.ProductCategories.Where(c => c.ParentCategoryId == null).ToListAsync();
        }

        public Task<bool> HasChildrenAsync(int categoryId)
        {
            return _context.ProductCategories.AnyAsync(c => c.ParentCategoryId == categoryId);
        }

        public async Task<bool> HasProductsAsync(int categoryId)
        {
            return await _context.Products.AnyAsync(p => p.CategoryId == categoryId);
        }

        public async Task<IEnumerable<ProductCategory>> SearchAsync(string term)
        {
            return await _context.ProductCategories
                .Where(c => c.CategoryName.Contains(term))
                .ToListAsync();
        }

       
    }
}
