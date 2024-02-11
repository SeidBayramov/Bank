using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cards = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();
            return View(cards);
        }
    }
}
