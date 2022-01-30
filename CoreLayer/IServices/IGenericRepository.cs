using DataLayer.Entities;
using System.Linq;

namespace CoreLayer.IServices
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        IQueryable<T> GetQueryable(int id);
        void Insert(T entity);
        void Update(T entity);
        bool Delete(T entity);
    }
}
