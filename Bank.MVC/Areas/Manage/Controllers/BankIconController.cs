using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BankIconController : Controller
    {
        private readonly IBankIconService _service;

        public BankIconController(IBankIconService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var Icons = await _service.GetAllAsync();
            return View(Icons);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateIconVm vm)
        {
            try
            {
                IconValidator validationRules = new IconValidator();
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
                await _service.CreateAsync(vm);
                return RedirectToAction("Index");
            }
            catch (ImageException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return View(vm);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                BankIcon BankIcon = await _service.GetByIdAsync(id);
                UpdateIconVm vm = new UpdateIconVm()
                {
                    Description = BankIcon.Description,
                    Id = BankIcon.Id,
                    TItle = BankIcon.Title,
                    SubTitle = BankIcon.SubTitle,
                    Icon = BankIcon.Icon
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
        public async Task<IActionResult> Update(UpdateIconVm vm)
        {
            try
            {
                UpdateValidator validationRules = new UpdateValidator();
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
                await _service.UpdateAsync(vm);
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