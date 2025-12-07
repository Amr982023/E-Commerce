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
    public class UserReviewRepo : GenericRepository<UserReview>, IUserReview
    {
        public UserReviewRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserReview>> GetRecentReviewsAsync(int productItemId, int limit = 10)
        {
            return await _context.Reviews
                .Where(ur => ur.OrderLine.ProductItemId == productItemId)
                .OrderByDescending(ur => ur.Id) // Assuming higher Id means more recent
                .Take(limit).ToListAsync();

        }

        public async Task<IEnumerable<UserReview>> GetReviewsByUserAsync(int userId)
        {
            return await _context.Reviews
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserReview>> GetReviewsForProductAsync(int productItemId)
        {
            return await _context.Reviews
                .Where(ur => ur.OrderLine.ProductItemId == productItemId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserReview>> GetReviewsWithUserAsync(int productId)
        {
            return await _context.Reviews
                .Include(ur => ur.User)
                .Where(ur => ur.OrderLine.ProductItemId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductItem>> GetTopRatedProductsAsync(int limit = 10)
        {
            return await _context.Reviews
                .GroupBy(ur => ur.OrderLine.ProductItem)
                .Select(g => new
                {
                    Product = g.Key,
                    AverageRating = g.Average(ur => ur.RatingValue)
                })
                .OrderByDescending(x => x.AverageRating)
                .Take(limit)
                .Select(x => x.Product)
                .ToListAsync();
        }

        public async Task<UserReview?> GetUserReviewAsync(int userId, int productItemId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.OrderLine.ProductItemId == productItemId);
        }
    }
}
