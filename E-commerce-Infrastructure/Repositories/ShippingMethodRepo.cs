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
    public class ShippingMethodRepo : GenericRepository<ShippingMethod>, IShippingMethod
    {
        public ShippingMethodRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ShippingMethod>> GetAvailableMethodsAsync()
        {
            return await _context.Set<ShippingMethod>().ToListAsync();
        }

        public async Task<ShippingMethod> GetCheapestAsync()
        {
            return await _context.ShippingMethods
         .OrderBy(sm => sm.Price)    
         .FirstOrDefaultAsync();

        }

        public async Task<ShippingMethod> GetMostExpensiveAsync()
        {
            return await _context.ShippingMethods.OrderByDescending(sm => sm.Price).FirstOrDefaultAsync();
        }
    }
}
