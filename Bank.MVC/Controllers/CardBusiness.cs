using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardBusiness : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
