using System.Linq;
using Store.Database.Entities;

namespace Store.Database.interfaces
{
    public interface IProduct2OrderRepository
    {
        void Add(Product2Order entity);
        IQueryable<Product2Order> GetAll();
    }
}