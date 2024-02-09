using AutoMapper;
using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Implementations;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Slider;
using Bank.Core.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly ISliderService _service;
        private readonly IWebHostEnvironment _env;


        public SliderController(ISliderService service,IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            var sliders = await _service.GetAllAsync();
            var sliderlist = sliders.ToList();
            return View(sliderlist);

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Create(CreateSliderVm vm)
        {
            try
            {
                CreateSliderValidator validationRules = new CreateSliderValidator();
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
                ModelState.AddModelError("Descriptions", "Descriptions is not bigger than 2000 words. Please try again.");
                return View(vm);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Slider portfolio = await _service.GetByIdAsync(id);
                UpdateSliderVm vm = new UpdateSliderVm()
                {
                    Descriptions = portfolio.Descriptions,
                    ImageUrl = portfolio.ImageUrl,
                    Id = portfolio.Id,
                    Title = portfolio.Title
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
        public async Task<IActionResult> Update(UpdateSliderVm vm)
        {
            try
            {
                UpdateSliderValidator validationRules = new UpdateSliderValidator();
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
            catch (ObjectNullException  ex)
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