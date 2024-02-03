using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly ISliderService _service;

        public SliderViewComponent(ISliderService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            return View(services);
        }
    }
}
