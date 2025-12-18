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
    public class UserRepo : GenericRepository<User>, IUser
    {
        public UserRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByAccountIdAsync(int accountId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Account.Id == accountId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Phone == phone);
        }

        public async Task<ShopOrder?> GetLastOrderForUserAsync(int userId)
        {
           return await _context.Orders
                .Where(o => o.User.Id == userId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetOrdersCountAsync(int userId)
        {
           return await _context.Orders
                .CountAsync(o => o.User.Id == userId);
        }

        public async Task<int> GetReviewsCountAsync(int userId)
        {
            return await _context.Reviews
                .CountAsync(r => r.User.Id == userId);
        }

        public async Task<User?> GetUserWithDetailsAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Account)
                .Include(u => u.Orders)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string? name, string? email, string? phone)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(name) ||
                    u.LastName.Contains(name) || u.FullName.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(u => u.Email.Contains(email));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(u => u.Phone.Contains(phone));
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
