using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Microsoft.AspNetCore.Mvc;

public class CardRequestController : Controller
{
    private readonly ICardRequestService _service;

    public CardRequestController(ICardRequestService service)
    {
        _service = service;
    }

    public IActionResult Confirm()
    {
        if (TempData.ContainsKey("ConfirmationMessage"))
        {
            ViewBag.ConfirmationMessage = TempData["ConfirmationMessage"].ToString();
        }

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

                // Set confirmation message in TempData to be displayed on the Confirm page
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
            return RedirectToAction("Index", "Home", new { errorMessage = "An error occurred: " + ex.Message });
        }
        catch (ObjectSameParamsException ex)
        {
            return RedirectToAction("Index", "Home", new { errorMessage = "Please check your information: " + ex.Message });
        }
    }
}
