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

        public void SaveChanges()
        {
            _storeContext.SaveChanges();
        }

        public IUserRepository UserRepository
        {
            get => _userRepository ?? (_userRepository = new UserRepository(_storeContext));
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
        private OrderRepository _orderRepository { get; set; }
        private ProductRepository _productRepository { get; set; }
    }
}
