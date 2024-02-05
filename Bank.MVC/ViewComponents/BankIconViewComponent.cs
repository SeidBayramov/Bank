using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.ViewComponents
{
    public class BankIconViewComponent : ViewComponent
    {
        private readonly IBankIconService _service;

        public BankIconViewComponent(IBankIconService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Icons = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            return View(Icons);
        }
    }
}
