using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
