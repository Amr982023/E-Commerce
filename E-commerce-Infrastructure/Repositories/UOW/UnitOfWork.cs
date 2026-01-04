using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Core.Interfaces;
using E_commerce_Core.Interfaces.Unit_Of_Work_Interface;
using E_commerce_Core.Repository_Interfaces;
using E_commerce_Infrastructure.Services;

namespace E_commerce_Infrastructure.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /* ===================== Services ===================== */

        private IEmailService _emailService;
        public IEmailService EmailService =>
            _emailService ??= new EmailService();

        /* ===================== Repositories ===================== */

        private IAccount _accounts;
        public IAccount Accounts =>
            _accounts ??= new AccountRepo(_context);

        private IAddress _addresses;
        public IAddress Addresses =>
            _addresses ??= new AddressRepo(_context);

        private ICountry _countries;
        public ICountry Countries =>
            _countries ??= new CountryRepo(_context);

        private IOrderLine _orderLines;
        public IOrderLine OrderLines =>
            _orderLines ??= new OrderLineRepo(_context);

        private IOrderStatus _orderStatuses;
        public IOrderStatus OrderStatuses =>
            _orderStatuses ??= new OrderStatusRepo(_context);

        private IPaymentMethod _paymentMethods;
        public IPaymentMethod PaymentMethods =>
            _paymentMethods ??= new PaymentMethodRepo(_context);

        private IPaymentType _paymentTypes;
        public IPaymentType PaymentTypes =>
            _paymentTypes ??= new PaymentTypeRepo(_context);

        private IProduct _products;
        public IProduct Products =>
            _products ??= new ProductRepo(_context);

        private IProductCategory _productCategories;
        public IProductCategory ProductCategories =>
            _productCategories ??= new ProductCategoryRepo(_context);

        private IProductConfiguration _productConfigurations;
        public IProductConfiguration ProductConfigurations =>
            _productConfigurations ??= new ProductConfigurationRepo(_context);

        private IProductItem _productItems;
        public IProductItem ProductItems =>
            _productItems ??= new ProductItemRepo(_context);

        private IPromotion _promotions;
        public IPromotion Promotions =>
            _promotions ??= new PromotionRepo(_context);

        private IPromotionCategory _promotionCategories;
        public IPromotionCategory PromotionCategories =>
            _promotionCategories ??= new PromotionCategoryRepo(_context);

        private IShippingMethod _shippingMethods;
        public IShippingMethod ShippingMethods =>
            _shippingMethods ??= new ShippingMethodRepo(_context);

        private IShopOrder _shopOrders;
        public IShopOrder ShopOrders =>
            _shopOrders ??= new ShopOrderRepo(_context);

        private IShoppingCart _shoppingCarts;
        public IShoppingCart ShoppingCarts =>
            _shoppingCarts ??= new ShoppingCartRepo(_context);

        private IShoppingCartItem _shoppingCartItems;
        public IShoppingCartItem ShoppingCartItems =>
            _shoppingCartItems ??= new ShoppingCartItemRepo(_context);

        private IUserReview _userReviews;
        public IUserReview UserReviews =>
            _userReviews ??= new UserReviewRepo(_context);

        private IVariation _variations;
        public IVariation Variations =>
            _variations ??= new VariationRepo(_context);

        private IVariationOption _variationOptions;
        public IVariationOption VariationOptions =>
            _variationOptions ??= new VariationOptionRepo(_context);

        private IUser _users;
        public IUser Users =>
            _users ??= new UserRepo(_context);

        /* ===================== Commit ===================== */

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /* ===================== Dispose ===================== */

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
