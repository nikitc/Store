using System.Linq;
using Microsoft.EntityFrameworkCore;
using Store.Database.interfaces;

namespace Store.Database
{
    public class CommonRepository<TEntity> : ICommonRepository<TEntity> where TEntity : class, IDbEntity
    {
        private readonly DbSet<TEntity> _objectSet;

        public CommonRepository(DbSet<TEntity> objectSet)
        {
            _objectSet = objectSet;
        }

        public void Create(TEntity entity)
        {
            _objectSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _objectSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _objectSet;
        }

        public TEntity GetById(int id)
        {
            return _objectSet.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            _objectSet.Update(entity);
        }
    }
}
