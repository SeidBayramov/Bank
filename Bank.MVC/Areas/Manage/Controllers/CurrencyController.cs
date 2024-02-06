using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Currency;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CurrencyController : Controller
    {
        private ICurrencyService _service;
        private readonly IWebHostEnvironment _env;
        public CurrencyController(ICurrencyService service, IWebHostEnvironment env = null)
        {
            _service = service;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var currencies = await _service.GetAllAsync();
            return View(currencies);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Create(CreateCurrencyVm vm)
        {
            try
            {
                CurrencyValidator validationRules = new CurrencyValidator();
                var result = await validationRules.ValidateAsync(vm);
                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(vm);
                }
                if (!ModelState.IsValid) { return View(vm); }
                await _service.CreateAsync(vm, _env.WebRootPath);
                return RedirectToAction("Index");
            }
            catch (ImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("Title", "Title is not bigger than 10 words. Please try again.");

                return View(vm);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Currency currency = await _service.GetByIdAsync(id);
                UpdateCurrencyVm vm = new UpdateCurrencyVm()
                {
                    SendMoney = currency.SendMoney,
                    RecieveMoney = currency.RecieveMoney,
                    ImageUrl = currency.ImageUrl,
                    Id = currency.Id,
                    Title = currency.Title
                };

                return View(vm);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Update(UpdateCurrencyVm vm)
        {
            try
            {
                UpdateCurrencyValidator validationRules = new UpdateCurrencyValidator();
                var result = await validationRules.ValidateAsync(vm);
                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    return View(vm);
                }
                if (!ModelState.IsValid) { return View(vm); }
                await _service.UpdateAsync(vm, _env.WebRootPath);
                return RedirectToAction("Index");
            }
            catch (ImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Update");
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Update");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("Title", "Title is not bigger than 10 words. Please try again.");

                return View(vm);
            }

        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            try
            {
                await _service.RecoverAsync(id);

                return RedirectToAction("Index");
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction("Index");
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return RedirectToAction("Index");
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction("Index");
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction("Index");
            }
        }
    }
}
