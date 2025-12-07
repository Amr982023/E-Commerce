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
    public class ShoppingCartRepo : GenericRepository<ShoppingCart>, IShoppingCart
    {
        public ShoppingCartRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddItemAsync(int ShoppingCartId, int productItemId, int qty)
        {
            await _context.ShoppingCartItems.AddAsync(new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                ProductItemId = productItemId,
                Qty = qty
            });
        }

        public async Task ClearCartAsync(int accountId)
        {
            await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId)
                .ExecuteDeleteAsync();
        } 

        public async Task<decimal> GetCartSubtotalAsync(int accountId)
        {
            return await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId)
                .SumAsync(sci => sci.Qty * sci.ProductItem.Price);
        }

        public async Task<ShoppingCart?> GetCartWithItemsAsync(int accountId)
        {
            return await _context.ShoppingCarts
                .Include(sc => sc.Items)
                .ThenInclude(sci => sci.ProductItem)
                .FirstOrDefaultAsync(sc => sc.AccountId == accountId);
        }

        public async Task<int> GetTotalQuantityAsync(int accountId)
        {
            return await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId)
                .SumAsync(sci => sci.Qty);
        }

        public async Task RemoveItemAsync(int accountId, int productItemId)
        {
           await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId)
                .ExecuteDeleteAsync();
        }

        public async Task UpdateItemQuantityAsync(int accountId, int productItemId, int qty)
        {
            await _context.ShoppingCartItems
                .Where(sci => sci.ShoppingCart.AccountId == accountId && sci.ProductItemId == productItemId)
                .ExecuteUpdateAsync(sci => sci.SetProperty(s => s.Qty, qty));
        }

    }
}
