using WebApplication.EfStuff.Models;

namespace WebApplication.EfStuff.Repository.IRepository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        User Get(string login);
    }
    
}