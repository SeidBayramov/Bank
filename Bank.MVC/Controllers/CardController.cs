using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
