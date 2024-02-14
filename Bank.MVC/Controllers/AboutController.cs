using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
