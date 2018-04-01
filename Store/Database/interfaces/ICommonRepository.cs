using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Database.interfaces
{
    public interface ICommonRepository<TEntity> where TEntity : IDbEntity
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Create(TEntity user);
        void Update(TEntity user);
        void Delete(TEntity user);
    }
}
