using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardBusinessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
