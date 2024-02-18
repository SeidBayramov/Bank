using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Bank.MVC.Controllers
{
    public class HomeController : Controller
    { 
        public async Task<IActionResult> Index(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        public async Task<IActionResult> AccessDeniedCustom()
        {
            return View();
        }
    }
}
