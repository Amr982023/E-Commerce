using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_Infrastructure.Repositories
{
    public class PaymentTypeRepo : IPaymentType
    {
        protected readonly ApplicationDbContext _context;
        public PaymentTypeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<E_commerce_Core.Models.PaymentType>> GetAllPaymentTypesAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task<E_commerce_Core.Models.PaymentType> GetByIdAsync(int id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }

        public async Task<E_commerce_Core.Models.PaymentType> GetByNameAsync(string name)
        {
            return await _context.PaymentTypes.FirstOrDefaultAsync(pt => pt.Type == name);
        }
    }
}
