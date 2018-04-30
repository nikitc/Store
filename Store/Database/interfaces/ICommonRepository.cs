using System.Linq;

namespace Store.Database.interfaces
{
    public interface ICommonRepository<TEntity> where TEntity : IDbEntity
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
