using Store.Database.interfaces;

namespace Store.Services
{
    public interface IDataManager
    {
        IUserRepository UserRepository { get; set; }
        IUserTokenRepository UserTokenRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IProduct2OrderRepository Product2OrderRepository { get; set; }

        void SaveChanges();
    }
}