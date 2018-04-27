using Store.Database.Entities;

namespace Store.Database.interfaces
{
    public interface IUserRepository : ICommonRepository<User>
    {
        User GetByName(string name);
    }
}
