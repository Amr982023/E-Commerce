using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Interfaces.Services;
using E_commerce_Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E_commerce_Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductItemService, ProductItemService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IOrderLineService, OrderLineService>();
            services.AddScoped<IShopOrderService, ShopOrderService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShoppingCartItemService, ShoppingCartItemService>();
            services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IShippingMethodService, ShippingMethodService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IVariationService, VariationService>();
            services.AddScoped<IVariationOptionService, VariationOptionService>();
            services.AddScoped<IProductConfigurationService, ProductConfigurationService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IPromotionCategoryService, PromotionCategoryService>();
            services.AddScoped<IUserReviewService, UserReviewService>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();

            return services;
        }
    }
}
