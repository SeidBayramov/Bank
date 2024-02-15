using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardCash : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
