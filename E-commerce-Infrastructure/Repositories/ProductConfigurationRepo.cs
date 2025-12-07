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
    public class ProductConfigurationRepo : GenericRepository<ProductConfiguration>, IProductConfiguration
    {
        public ProductConfigurationRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddConfigurationsAsync(int productItemId, IEnumerable<int> optionIds)
        {
           await _context.ProductConfigurations.AddRangeAsync(
                optionIds.Select(optionId => new ProductConfiguration
                {
                    ProductItemId = productItemId,
                    VariationOptionId = optionId
                })
            );
        }

        public async Task<bool> ExistsAsync(int productItemId, int variationOptionId)
        {
          return await _context.ProductConfigurations.AnyAsync(pc => pc.ProductItemId == productItemId && pc.VariationOptionId == variationOptionId);
        }

        public async Task<IEnumerable<ProductConfiguration>> GetByProductItemAsync(int productItemId)
        {
            return await _context.ProductConfigurations
                .Where(pc => pc.ProductItemId == productItemId)
                .ToListAsync();
        }

        public async Task<IEnumerable<VariationOption>> GetOptionsForProductAsync(int productId)
        {
            return await _context.ProductConfigurations
                .Where(pc => pc.ProductItem.ProductId == productId)
                .Select(pc => pc.VariationOption)
                .Distinct()
                .ToListAsync();
        }

        public async Task<ProductItem?> GetProductItemByOptionsAsync(int productId, IEnumerable<int> optionIds)
        {
            return await _context.ProductItems
                .Where(pi => pi.ProductId == productId)
                .Where(pi => pi.Configurations
                    .Select(pc => pc.VariationOptionId)
                    .OrderBy(id => id)
                    .SequenceEqual(optionIds.OrderBy(id => id)))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductItem>> GetProductItemsByOptionAsync(int variationOptionId)
        {
            return await _context.ProductConfigurations
                .Where(pc => pc.VariationOptionId == variationOptionId)
                .Select(pc => pc.ProductItem)
                .ToListAsync();
        }

        public async Task RemoveConfigurationsForProductItemAsync(int productItemId)
        {
            await _context.ProductConfigurations
                .Where(pc => pc.ProductItemId == productItemId)
                .ExecuteDeleteAsync();
        }

    }
}
