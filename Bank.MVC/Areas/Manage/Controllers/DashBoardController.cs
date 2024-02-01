using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DashBoardController : Controller
    {
        public async Task<IActionResult>Index()
        {
            return View();
        }
    }
}
