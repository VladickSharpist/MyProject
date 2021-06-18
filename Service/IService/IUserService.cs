using System.Security.Claims;
using WebApplication.EfStuff.Models;

namespace WebApplication.Service.IService
{
    public interface IUserService
    {
        ClaimsPrincipal GetPrincipal(User user);
        User GetCurrent();
    }
}