using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Bank.MVC.Controllers
{
    public class HomeController : Controller
    { private readonly IBankIconService _iconService;

        public HomeController(IBankIconService iconService)
        {
            _iconService = iconService;
        }

        public async Task<IActionResult> Index()
        {
            var icons = (await _iconService.GetAllAsync()).Where(x => !x.IsDeleted).ToList();
            return View(icons);
        }

        public async Task<IActionResult> AccessDeniedCustom()
        {
            return View();
        }
    }
}
