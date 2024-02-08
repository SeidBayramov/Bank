using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardCashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
