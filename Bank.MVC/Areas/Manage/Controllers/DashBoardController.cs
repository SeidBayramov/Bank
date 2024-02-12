using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class DashBoardController : Controller
    {
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult>Index()
        {
            return View();
        }
    }
}
