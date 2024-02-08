using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class DepositController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
