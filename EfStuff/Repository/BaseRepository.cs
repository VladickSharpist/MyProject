using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.EfStuff.Models;
using WebApplication.EfStuff.Repository.IRepository;

namespace WebApplication.EfStuff.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T:BaseModel
    {
        protected ProjectDbContext _dbContext;
        protected DbSet<T> _dbSet;
        
        public BaseRepository(ProjectDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public virtual void Save(T model)
        {
            if (model.Id > 0)
            {
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }
            _dbContext.SaveChanges();
        }
        
        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }
        
        public virtual void Remove(T model)
        {
            _dbContext.Remove(model);
            _dbContext.SaveChanges();
        }
        public virtual void Remove(long id)
        {
            var model = Get(id);
            Remove(model);
        }

        public virtual void Remove(IEnumerable<long> ids)
        {
            foreach (var userid in ids)
            {
                Remove(userid);
            }
        }
    }
}