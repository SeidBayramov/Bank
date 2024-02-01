using Bank.Business.ViewModels.Account;
using Bank.Core.Entities.Account;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _appDb;

        public AccountController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            SignInManager<AppUser> signInManager, AppDbContext appDb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appDb = appDb;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            return View();
        }
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

    }
}
