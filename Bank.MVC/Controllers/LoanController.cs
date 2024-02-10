using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
