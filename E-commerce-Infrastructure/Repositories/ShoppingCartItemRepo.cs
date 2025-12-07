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
    public class ShoppingCartItemRepo : GenericRepository<ShoppingCartItem>, IShoppingCartItem
    {
        public ShoppingCartItemRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ShoppingCartItem?> GetCartItemAsync(int accountId, int productItemId)
        {
            return await _context.ShoppingCartItems
                .FirstOrDefaultAsync(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetItemsAsync(int accountId)
        {
            return await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetItemsWithDetailsAsync(int accountId)
        {
            return await _context.ShoppingCartItems
                .Include(sci => sci.ProductItem)
                .Where(sci => sci.ShoppingCart.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<decimal> GetItemTotalPriceAsync(int accountId, int productItemId)
        {
            return await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId)
                .Select(sci => sci.Qty * sci.ProductItem.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<ShoppingCartItem?> GetItemWithDetailsAsync(int accountId, int productItemId)
        {
             return await _context.ShoppingCartItems
                .Include(sci => sci.ProductItem)
                .FirstOrDefaultAsync(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId);
        }

        public async Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty)
        {
            await _context.ShoppingCartItems.Where(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId)
                .ExecuteUpdateAsync(sci => sci.SetProperty(x=>x.Qty,qty));
        }
    }
}
