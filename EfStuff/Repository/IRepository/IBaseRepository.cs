using System.Collections.Generic;
using WebApplication.EfStuff.Models;

namespace WebApplication.EfStuff.Repository.IRepository
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        void Save(T model);
        List<T> GetAll();
        T Get(long id);
        void Remove(T model);
        void Remove(long id);
        void Remove(IEnumerable<long> ids);
    }
}