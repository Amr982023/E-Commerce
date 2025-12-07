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
    public class OrderLineRepo : GenericRepository<OrderLine>, IOrderLine
    {
        public OrderLineRepo(ApplicationDbContext context) : base(context)
        {
        }

        public decimal CalculateLineTotal(OrderLine line)
        {
            return line.Qty * line.Price;
        }

        public Task<OrderLine> GetOrderLineAsync(int orderId, int productItemId)
        {
            return _context.OrderLines.FirstOrDefaultAsync(ol => ol.ShopOrderId == orderId && ol.ProductItemId == productItemId);
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLinesByOrderIdAsync(int orderId)
        {
            return await _context.OrderLines.Where(ol => ol.ShopOrderId == orderId).ToListAsync();
        }

        public Task<OrderLine> GetOrderLineWithDetailsAsync(int orderId, int productItemId)
        {
            return _context.OrderLines.Where(ol => ol.ShopOrderId == orderId && ol.ProductItemId == productItemId)
                .Include(ol=>ol.ShopOrder)
                .Include(ol => ol.ProductItem)
                .Include(ol => ol.UserReviews).FirstOrDefaultAsync();
        }

    }
}
