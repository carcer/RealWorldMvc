using System.Collections.Generic;
using System.Linq;

namespace RealWorldMvc3.Core.Repositories
{
    public interface IRepository
    {
        T FindById<T>(object id);

        T LoadForId<T>(object id);

        IEnumerable<T> FindAll<T>();

        IQueryable<T> Query<T>();

        void Save<T>(T entity);

        void Delete<T>(T entity);
    }
}