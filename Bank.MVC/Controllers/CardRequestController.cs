using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    public class CardRequestController : Controller
    {
        private readonly ICardRequestService _service;

        public CardRequestController(ICardRequestService service)
        {
            _service = service;
        }

        public IActionResult Confirm(string message)
        {
            ViewBag.ConfirmationMessage = message;
            return View();
        }


        public async Task<IActionResult> Apply(CardRequestVm vm)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var validate = new RequestVMValidator();
                    var result = validate.Validate(vm);

                    if (!result.IsValid)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }

                        return RedirectToAction("Index", "Home", new { errorMessage = "Please check your FinCode or Email, and make sure you click the button" });
                    }

                    await _service.Apply(vm);

                    return RedirectToAction("Confirm", new { message = "Check your email for confirmation." });
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (ObjectParamsNullException ex)
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "An error occurred: " + ex.Message });
            }
            catch (ObjectSameParamsException ex)
            {
                return RedirectToAction("Index", "Home", new { errorMessage = "Please check your information: " + ex.Message });
            }
        }
    }
}
