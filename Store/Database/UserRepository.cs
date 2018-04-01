using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class UserRepository : CommonRepository<User>, IUserRepository
    {
        public UserRepository(StoreContext context) : base (context.Users)
        {
        }
    }
}
