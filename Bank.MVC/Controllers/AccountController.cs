using Bank.Business.Exceptions.Account;
using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Implementations;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Account;
using Bank.Core.Entities.Account;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _http;

        public AccountController(IAccountService service, LinkGenerator linkGenerator, IHttpContextAccessor http)
        {
            _service = service;
            _linkGenerator = linkGenerator;
            _http = http;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm  vm)
        {

            try
            {
                RegisterVmValidation validationRules = new RegisterVmValidation();
                var result = validationRules.Validate(vm);
                if (!result.IsValid)
                {
                    foreach (var eror in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                    }
                    return View(vm);
                }

               var response= await _service.Register(vm);
                var token = response[0];
                var pincode = response[1];
                var userId = response[2];

                string url = _linkGenerator.GetUriByAction(_http.HttpContext, action: "ConfirmEmail", controller: "Account",
                 values: new
                 {
                     token,
                     userId,
                     pincode
                 });

                return Redirect(url);

            }
            catch (UsedEmailException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (UserRegistrationException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (!User.Identity.IsAuthenticated)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm, string? returnUrl)
        {
            try
            {
                LoginVmValidation validationRules = new LoginVmValidation();
                var result = validationRules.Validate(vm);
                if (!result.IsValid)
                {
                    foreach (var eror in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(eror.PropertyName, eror.ErrorMessage);
                    }
                    return View(vm);
                }
                await _service.Login(vm);

                if (returnUrl is not null) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _service.Logout();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRoles();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfrimEmailVm vm, string userId, string token, string pincode)
        {
            ViewBag.Success = true;

            if (await _service.ConfirmEmailAddress(vm: vm, userId: userId, token: token, pincode: pincode))
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ViewBag.Success = false;
            }

            return View(vm);
        }

        //    [HttpPost]
        //    public async Task<IActionResult> Subscription(SubscribeVM vm, string? returnUrl)
        //    {
        //        try
        //        {
        //            await _accountService.Subscription(vm);

        //            if (returnUrl is not null) return Redirect(returnUrl);

        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (UsedEmailException ex)
        //        {
        //            ModelState.AddModelError(ex.ParamName, ex.Message);

        //            if (returnUrl is not null) return Redirect(returnUrl);

        //            return RedirectToAction("Index", "Home", vm);
        //        }
        //    }
        //}
    }
}