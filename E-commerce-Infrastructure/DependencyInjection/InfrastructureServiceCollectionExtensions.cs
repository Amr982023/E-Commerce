using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Infrastructure.Repositories;
using E_commerce_Infrastructure.Repositories.Generic;
using E_commerce_Infrastructure.Repositories.UOW;
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

            // Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories    
            services.AddScoped<IAccount, AccountRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IProduct, ProductRepo>();
            services.AddScoped<IProductItem, ProductItemRepo>();
            services.AddScoped<IProductCategory, ProductCategoryRepo>();
            services.AddScoped<IOrderLine, OrderLineRepo>();
            services.AddScoped<IShopOrder, ShopOrderRepo>();
            services.AddScoped<IShoppingCart, ShoppingCartRepo>();
            services.AddScoped<IShoppingCartItem, ShoppingCartItemRepo>();
            services.AddScoped<IAddress, AddressRepo>();
            services.AddScoped<IPaymentMethod, PaymentMethodRepo>();
            services.AddScoped<IPaymentType, PaymentTypeRepo>();
            services.AddScoped<IShippingMethod, ShippingMethodRepo>();
            services.AddScoped<ICountry, CountryRepo>();
            services.AddScoped<IVariation, VariationRepo>();
            services.AddScoped<IVariationOption, VariationOptionRepo>();
            services.AddScoped<IProductConfiguration, ProductConfigurationRepo>();
            services.AddScoped<IPromotion, PromotionRepo>();
            services.AddScoped<IPromotionCategory, PromotionCategoryRepo>();
            services.AddScoped<IUserReview, UserReviewRepo>();
            services.AddScoped<IOrderStatus, OrderStatusRepo>();

            return services;
        }

    }
}
