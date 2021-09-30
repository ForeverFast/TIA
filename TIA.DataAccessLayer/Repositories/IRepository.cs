using System;

namespace TIA.DataAccessLayer.Repositories
{
    public interface IRepository<T>
    {
        T GetById(Guid id);

        T Add(T entity);

        T Update(T entity, Guid id);

        bool Delete(Guid id);
    }
}
