using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using WebApplication.EfStuff.Models;
using WebApplication.EfStuff.Repository.IRepository;
using WebApplication.Service.IService;

namespace WebApplication.Service
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _contextAccessor;
        private IUserRepository _userRepository;

        public UserService(IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }
        
        public ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(
                ClaimTypes.AuthenticationMethod,
                Startup.AuthMethod));
            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthMethod);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return principal;
        }

        public User GetCurrent()
        {
            var idStr = _contextAccessor.HttpContext?.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value;

            if (idStr.IsNullOrEmpty())
            {
                return null;
            }

            var id = long.Parse(idStr);
            return _userRepository.Get(id);
        }
    }
}