using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.EfStuff.Models;
using WebApplication.EfStuff.Repository;
using WebApplication.EfStuff.Repository.IRepository;
using WebApplication.Models;
using WebApplication.Service.IService;

namespace WebApplication.Controllers
{
    public class UserController:Controller
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private IUserService _userService;

        public UserController(IMapper mapper, IUserRepository userRepository, IUserService userService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = _userRepository.Get(viewModel.Login);

            if (user == null || user.Password!=viewModel.Password)
            {
                return View(viewModel);
            }

            await HttpContext.SignInAsync(_userService.GetPrincipal(user));
            
            return RedirectToAction("Cabinet","User");
        }
        
        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegistrationViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var isUserUniq = _userRepository.Get(viewModel.Login) == null;

            if (isUserUniq)
            {
                var user = _mapper.Map<User>(viewModel);
                _userRepository.Save(user);
                
                await HttpContext.SignInAsync(
                    _userService.GetPrincipal(user));
                
                return RedirectToAction("Cabinet", "User");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Home", "Home");
        }
        
        [Authorize]
        public IActionResult Cabinet()
        {
            var model = new CabinetViewModel();
            return View(model);
        }
    }
    
}