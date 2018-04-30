using Store.Database;
using Store.Database.interfaces;

namespace Store.Services
{
    public class DataManager : IDataManager
    {
        private readonly StoreContext _storeContext;

        public DataManager(StoreContext context)
        {
            _storeContext = context;
        }

        public IProduct2OrderRepository Product2OrderRepository
        {
            get => _product2OrderRepository ?? (_product2OrderRepository = new Product2OrderRepository(_storeContext));
            set { }
        }

        public IImageRepository ImageRepository
        {
            get => _imageRepository ?? (_imageRepository = new ImageRepository(_storeContext));
            set { }
        }

        public IBrandRepository BrandRepository
        {
            get => _brandRepository ?? (_brandRepository = new BrandRepository(_storeContext));
            set { }

        }

        public void SaveChanges()
        {
            _storeContext.SaveChanges();
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ?? (_userRepository = new UserRepository(_storeContext));
            set { }
        }

        public IUserTokenRepository UserTokenRepository
        {
            get => _userTokenRepository ?? (_userTokenRepository = new UserTokenRepository(_storeContext));
            set { }
        }

        public IOrderRepository OrderRepository
        {
            get => _orderRepository ?? (_orderRepository = new OrderRepository(_storeContext));
            set { }
        }

        public IProductRepository ProductRepository
        {
            get => _productRepository ?? (_productRepository = new ProductRepository(_storeContext));
            set { }
        }

        private UserRepository _userRepository { get; set; }
        private UserTokenRepository _userTokenRepository { get; set; }
        private OrderRepository _orderRepository { get; set; }
        private ProductRepository _productRepository { get; set; }
        private Product2OrderRepository _product2OrderRepository { get; set; }
        private ImageRepository _imageRepository { get; set; }
        private BrandRepository _brandRepository { get; set; }
    }
}
