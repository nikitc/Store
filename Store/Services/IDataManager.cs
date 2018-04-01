using Store.Database.interfaces;

namespace Store.Services
{
    public interface IDataManager
    {
        IUserRepository UserRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IProductRepository ProductRepository { get; set; }

        void SaveChanges();
    }
}