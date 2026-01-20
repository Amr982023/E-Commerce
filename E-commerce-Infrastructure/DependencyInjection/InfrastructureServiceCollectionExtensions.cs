using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Interfaces.Security;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Repository_Interfaces;
using E_commerce_Infrastructure.Repositories;
using E_commerce_Infrastructure.Repositories.Generic;
using E_commerce_Infrastructure.Repositories.JWT;
using E_commerce_Infrastructure.Repositories.UOW;
using E_commerce_Infrastructure.Security;
using E_commerce_Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_commerce_Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)));


            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Email Service
            services.AddScoped<IEmailService, EmailService>();
          
            return services;
        }

    }
}
