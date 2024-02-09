using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.Business.ViewModels.Category;
using Bank.Core.Entities.Models;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Bank.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CardController : Controller
    {
        private readonly ICardService _cardservice;
        private readonly ICategoryService _categoryservice;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IFeatureService _featureservice;

        public CardController(ICardService cardservice, ICategoryService categoryservice, AppDbContext context, IFeatureService featureservice, IWebHostEnvironment env)
        {
            _cardservice = cardservice;
            _categoryservice = categoryservice;
            _context = context;
            _featureservice = featureservice;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var cards = await _cardservice.GetAllAsync();
            var cardList = cards.ToList();
            return View(cardList);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCardVm vm)
        {
            try
            {
                await _cardservice.CreateAsync(vm, _env.WebRootPath);

                return RedirectToAction(nameof(Index));
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
            catch (ObjectSameParamsException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Card oldProduct = await _cardservice.GetByIdAsync(id);

                UpdateCardVm vm = new()
                {
                    Title = oldProduct.Title,
                    Description = oldProduct.Description,
                    IsInStock = oldProduct.IsInStock,
                    CategoryId = oldProduct.CategoryId,
                    CardImageVms = new List<CardsmageVm>(),
                    FeaturesIds = oldProduct.CardFeatures.Select(x => x.FeatureId).ToList(),
                };

                foreach (var item in oldProduct.CardImages)
                {
                    CardsmageVm cardsmageVm = new()
                    {
                        Id = item.Id,
                        ImageUrl = item.ImageUrl,
                    };

                    vm.CardImageVms.Add(cardsmageVm);
                }


                return View(vm);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateCardVm vm)
        {
            try
            {
                await _cardservice.UpdateAsync(vm, _env.WebRootPath);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
            catch (ObjectSameParamsException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
        }

        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cardservice.DeleteAsync(id);
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
                await _cardservice.RecoverAsync(id);

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
                await _cardservice.RemoveAsync(id);

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