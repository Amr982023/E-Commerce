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
    public class ShopOrderRepo : GenericRepository<ShopOrder>, IShopOrder
    {
        public ShopOrderRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var Order = await _context.Orders.FindAsync(orderId);

            if (Order != null)
                Order.OrderStatusId = (int)enOrderStatus.Cancelled;
        }

        public async Task ConfirmOrderAsync(int orderId)
        {
            var Order = await _context.Orders.FindAsync(orderId);

            if (Order != null)
                Order.OrderStatusId = (int)enOrderStatus.Confirmed;
        }

        public async Task<decimal> GetAverageOrderValueAsync()
        {
            return await _context.Orders.AverageAsync(o => o.OrderTotalCost);
        }

        public async Task<ShopOrder?> GetLastOrderAsync(int accountId)
        {
            return await _context.Orders
                .Where(o => o.UserId == accountId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShopOrder>> GetOrdersByStatusAsync(enOrderStatus status)
        {
            return await _context.Orders
                .Where(o => o.OrderStatusId == (int)status)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShopOrder>> GetOrdersForAccountAsync(int accountId)
        { 
            var Userid = await _context.Accounts
                .Where(a => a.Id == accountId).Select(a => a.UserId).FirstOrDefaultAsync();
              

            return await _context.Orders
                .Where(o => o.UserId == Userid)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShopOrder>> GetOrdersForAccountPagedAsync(int accountId, int page, int size)
        {
            var Userid = await _context.Accounts
                .Where(a => a.Id == accountId).Select(a => a.UserId).FirstOrDefaultAsync();


            return await _context.Orders
                .Where(o => o.UserId == Userid).Skip((page - 1) * size).Take(size)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShopOrder>> GetOrdersInRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Orders
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .ToListAsync();
        }

        public async Task<ShopOrder> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductItem)
                .Include(o => o.ShippingAddress)
                .Include(o => o.PaymentMethod)
                .Include(o => o.ShippingMethod)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<ShopOrder>> GetPendingOrdersAsync()
        {
           return await _context.Orders
                .Where(o => o.OrderStatusId == (int)enOrderStatus.Pending)
                .ToListAsync();
        }

        public async Task<decimal> GetTodaySalesAsync()
        {
            return await _context.Orders
                .Where(o => o.OrderDate.Date == DateTime.Today)
                .SumAsync(o => o.OrderTotalCost);
        }

        public async Task SetPaymentMethodAsync(int orderId, int paymentMethodId)
        {
            var Order = await _context.Orders.FindAsync(orderId);
            if (Order != null)
            {
                Order.PaymentMethodId = paymentMethodId;
                _context.Update(Order);
            }
        }

        public async Task SetShippingMethodAsync(int orderId, int shippingMethodId)
        {
            var Order = await _context.Orders.FindAsync(orderId);
            if (Order != null)
            {
                Order.ShippingMethodId = shippingMethodId;
                _context.Update(Order);
            }
        }

    }
}
