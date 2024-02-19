using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Loan;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _service;

        public LoanController(ILoanService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Confirm(string message)
        {
            ViewBag.ConfirmationMessage = message;
            return View();
        }

        public async Task<IActionResult> Apply(LoanVm vm)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var validate = new LoanVmValidator();
                    var result = validate.Validate(vm);
                    if (!result.IsValid)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }

                        return RedirectToAction("Index");
                    }

                    await _service.Send(vm);

                    return RedirectToAction("Confirm", new { message = "Check your email for confirmation." });


                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
            catch (ObjectSameParamsException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
