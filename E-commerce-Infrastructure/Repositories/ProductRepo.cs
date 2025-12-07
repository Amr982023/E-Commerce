using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Consts;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using E_commerce_Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class ProductRepo : GenericRepository<Product>, IProduct
    {
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetBestSellingProductsAsync(int limit = 10)
        {
            return await _context.OrderLines
        .Where(ol => ol.ShopOrder.Status.Id == (int)E_commerce_Core.Consts.enOrderStatus.Confirmed)
        .GroupBy(ol => ol.ProductItem.ProductId)
        .Select(g => new
        {
            ProductId = g.Key,
            TotalSold = g.Sum(x => x.Qty)
        })
        .OrderByDescending(x => x.TotalSold)
        .Take(limit)
        .Join(_context.Products,
              g => g.ProductId,
              p => p.Id,
              (g, p) => p)
        .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetProductWithDetails(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductItems)
                .ThenInclude(pi => pi.Configurations)
                .ThenInclude(c => c.VariationOption)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<bool> ProductExistsAsync(int productId)
        {
            return _context.Products.AnyAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string term, int limit = 20)
        {
           return await _context.Products
                .Where(p => p.Name.Contains(term) || p.Description.Contains(term))
                .Take(limit)
                .ToListAsync();
        }

    }
}
