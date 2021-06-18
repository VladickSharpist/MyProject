using System.Linq;
using WebApplication.EfStuff.Models;
using WebApplication.EfStuff.Repository.IRepository;

namespace WebApplication.EfStuff.Repository
{
    public class UserRepository:BaseRepository<User>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {
        }

        public User Get(string login)
        {
            return _dbSet.SingleOrDefault(x => x.Login == login);
        }
    }
}