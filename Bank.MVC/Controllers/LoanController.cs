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
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            return View();
        }

        public IActionResult Confirm(string message)
        {
            if (TempData.ContainsKey("ConfirmationMessage"))
            {
                ViewBag.ConfirmationMessage = TempData["ConfirmationMessage"].ToString();
            }

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

                        TempData["ErrorMessage"] = "An error occurred while processing your request.";
                        return RedirectToAction("Index");
                    }

                    await _service.Send(vm);

                    TempData["ConfirmationMessage"] = "Your request has been successfully submitted. Check your email for confirmation. An email will be sent shortly.";

                    return RedirectToAction("Confirm");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                TempData["ErrorMessage"] = "Check your input and try again.";
            }
            catch (ObjectSameParamsException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                TempData["ErrorMessage"] = "The FinCode or Email has been used before.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request.";
            }

            return RedirectToAction("Index");
        }
    }
}
