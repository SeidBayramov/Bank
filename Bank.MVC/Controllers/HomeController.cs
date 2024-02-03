using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks; // Add this line

namespace Bank.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _service;

        public HomeController(ISliderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            return View(sliders);
        }

        public async Task<IActionResult> AccessDeniedCustom()
        {
            return View();
        }
    }
}
