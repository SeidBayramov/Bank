using Bank.Business.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.ViewComponents
{
    public class QuestionViewComponent:ViewComponent
    {
        private readonly IQuestionService _service;

        public QuestionViewComponent(IQuestionService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var question = (await _service.GetAllAsync()).Where(x => !x.IsDeleted).ToList();

            return View(question);
        }
    }
}
