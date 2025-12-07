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
    public class PromotionRepo : GenericRepository<Promotion>, IPromotion
    {
        public PromotionRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
        {
            return await _context.Promotions
                .Where(p => p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now)
                .ToListAsync();
        }

        public async Task<decimal> GetDiscountForProductItemAsync(int productItemId)
        {
            var now = DateTime.UtcNow;

            var discount = await (
                from pi in _context.ProductItems
                join p in _context.Products
                    on pi.ProductId equals p.Id
                join pc in _context.PromotionCategories
                    on p.CategoryId equals pc.CategoryId
                join promo in _context.Promotions
                    on pc.PromotionId equals promo.Id
                where pi.Id == productItemId
                      && promo.StartDate <= now
                      && promo.EndDate >= now
            
                orderby promo.DiscountRate descending  
                select promo.DiscountRate
            ).FirstOrDefaultAsync();   

            return discount;
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsForCategoryAsync(int categoryId)
        {
            return await (from Promo in _context.Promotions
                         join pc in _context.PromotionCategories
                         on Promo.Id equals pc.PromotionId
                         where pc.CategoryId == categoryId
                         select Promo).Distinct().ToListAsync();

        }

        public async Task<IEnumerable<Promotion>> GetPromotionsForProductAsync(int productId)
        {
            var Query = from prom in _context.Promotions
                    join pc in _context.PromotionCategories
                    on prom.Id equals pc.PromotionId

                    join cat in _context.ProductCategories
                    on pc.CategoryId equals cat.Id

                    join Product in _context.Products
                    on cat.Id equals Product.CategoryId

                    where Product.Id == productId
                    select prom;


            return await Query.Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsForProductItemAsync(int productItemId)
        {
            return await (from Promo in _context.Promotions
                          join pc in _context.PromotionCategories
                           on Promo.Id equals pc.PromotionId

                          join Product in _context.Products
                          on pc.CategoryId equals Product.CategoryId

                          join pi in _context.ProductItems
                            on Product.Id equals pi.ProductId

                          where pi.Id == productItemId
                          select Promo).Distinct().ToListAsync();
        }

        public bool IsExpired(Promotion promo)
        {
            return promo.EndDate < DateTime.Now;
        }

    }
}
