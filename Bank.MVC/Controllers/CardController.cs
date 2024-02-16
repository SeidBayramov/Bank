using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Bank.MVC.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? sortby)
        {
            var cards = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            switch (sortby)
            {
                case 1: 
                    cards = cards.OrderByDescending(card => card.CardFeatures.Count()).ToList();
                    break;
                case 2: 
                    cards = cards.OrderBy(card => card.Id).ToList();
                    break;
                default:
                    cards = cards.OrderByDescending(card => card.Id).ToList();
                    break;
            }


            return View(cards);
        }
    }
}
