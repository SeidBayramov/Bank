using Bank.Business.Services.Interface;
using Bank.Core.Entities.Models;
using Bank.MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.MVC.Controllers
{
    public class CardSingleController : Controller
    {
        private readonly ICardService _service;

        public CardSingleController(ICardService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            var card =  await (await _service.GetAllAsync())
                .Where(x => !x.IsDeleted && x.Id==id)
                .Include(x => x.CardImages)
                .SingleOrDefaultAsync();


            if (card == null)
            {
                return RedirectToAction("NotFound", "Error"); 
            }


            return View(card);
        }

    }
}
