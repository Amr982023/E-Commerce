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
    public class PaymentMethodRepo : GenericRepository<PaymentMethod>, IPaymentMethod
    {
        public PaymentMethodRepo(ApplicationDbContext context) : base(context)
        {
        }

        public Task<PaymentMethod> GetByPaymentMethodWithDetailsAsync(int id)
        {
            return _context.PaymentMethods
                .Include(pm => pm.Account)
                .Include(pm => pm.PaymentType)
                .FirstOrDefaultAsync(pm => pm.Id == id);
        }

        public async Task<IEnumerable<E_commerce_Core.Models.PaymentMethod>> GetByProviderAsync(string provider)
        {
            return await _context.PaymentMethods
                .Where(pm => pm.Provider == provider)
                .ToListAsync();
        }

        public async Task<E_commerce_Core.Models.PaymentMethod> GetDefaultPaymentMethodAsync(int accountId)
        {
           return await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.AccountId == accountId && pm.IsDefault);
        }

        public async Task<IEnumerable<E_commerce_Core.Models.PaymentMethod>> GetPaymentMethodsByAccountAsync(int accountId)
        {
            return await _context.PaymentMethods
                .Where(pm => pm.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<E_commerce_Core.Models.PaymentMethod>> GetValidPaymentMethodsAsync(int accountId)
        {
            return await _context.PaymentMethods
                .Where(pm => pm.AccountId == accountId && pm.ExpiryDate >= DateTime.Now)
                .ToListAsync();
        }

        public bool IsExpired(E_commerce_Core.Models.PaymentMethod method)
        {
            return method.ExpiryDate < DateTime.Now;
        }

        public string MaskCardNumber(string number) //remove it to service utils
        {
            if (string.IsNullOrWhiteSpace(number) || number.Length <= 4)
                return number; 

            string last4 = number.Substring(number.Length - 4);
            string masked = new string('*', number.Length - 4);

            return masked + last4;
        }

        public Task<bool> PaymentMethodBelongsToAccountAsync(int accountId, int paymentMethodId)
        {
            return _context.PaymentMethods
                .AnyAsync(pm => pm.Id == paymentMethodId && pm.AccountId == accountId);
        }

        public async Task SetDefaultPaymentMethodAsync(int accountId, int paymentMethodId)
        {
            await _context.PaymentMethods.Where(pm => pm.AccountId == accountId && pm.IsDefault)
                .ExecuteUpdateAsync(pm => pm.SetProperty(x => x.IsDefault, false));

            await _context.PaymentMethods.Where(pm => pm.Id == paymentMethodId && pm.AccountId == accountId)
                .ExecuteUpdateAsync(pm => pm.SetProperty(x => x.IsDefault, true));

        }

    }
}
