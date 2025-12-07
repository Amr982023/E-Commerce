using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class OrderStatusRepo : IOrderStatus
    {
        protected readonly ApplicationDbContext _context;
        public OrderStatusRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<E_commerce_Core.Models.OrderStatus>> GetAllStatusesAsync()
        {
            return await _context.OrderStatuses.ToListAsync();
        }
    }
}
