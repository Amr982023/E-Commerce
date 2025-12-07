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
    public class CountryRepo : ICountry
    {
        protected readonly ApplicationDbContext _context;
        public CountryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByCodeAsync(string Code)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Code == Code);
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

    }
}
