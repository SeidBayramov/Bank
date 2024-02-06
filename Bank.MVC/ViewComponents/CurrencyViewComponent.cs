using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.ViewComponents
{
    public class CurrencyViewComponent:ViewComponent
    {
        private readonly ICurrencyService _service;

        public CurrencyViewComponent(ICurrencyService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Sliders = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            return View(Sliders);
        }
    }
}
