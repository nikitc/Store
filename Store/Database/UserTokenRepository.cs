using Store.Database.Entities;
using Store.Database.interfaces;
using Store.Services;

namespace Store.Database
{
    public class UserTokenRepository : CommonRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(StoreContext context) : base(context.UserTokens)
        {
        }
    }
}
