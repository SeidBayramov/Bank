using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.Business.ViewModels.Category;
using Bank.Core.Entities.Models;
using Bank.DAL.Context;
using Bank.MVC.PaginationHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Features = await _context.Features.AsNoTracking().ToListAsync();
            var query = _context.Cards.AsQueryable();
            PaginatedList<Card> paginatedList = new PaginatedList<Card>(query.Skip((page - 1) * 2).Take(2).ToList(), page, query.ToList().Count, 2);
            if (page > paginatedList.TotalPageCount)
            {
                paginatedList = new PaginatedList<Card>(query.Skip((page - 1) * 2).Take(2).ToList(), page, query.ToList().Count, 2);
            }
            return View(paginatedList);
        }
        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                Card oldcard = await _cardservice.GetByIdAsync(id);

                return View(oldcard);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Index));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryservice.GetAllAsync();
            ViewBag.Features = await _featureservice.GetAllAsync();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCardVm vm)
        {
            try
            {

                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Features = await _context.Features.AsNoTracking().ToListAsync();
                await _cardservice.CreateAsync(vm, _env.WebRootPath);

                return RedirectToAction(nameof(Index));
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View(vm);
            }
            catch(ImageException ex)
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ViewBag.Categories = await _context.Categories.AsNoTracking().ToListAsync();
                ViewBag.Features = await _context.Features.AsNoTracking().ToListAsync();
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

                return RedirectToAction(nameof(Index));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateCardVm vm)
        {
            try
            {
                ViewBag.Categories = await _context.Categories.AsNoTracking().ToListAsync();
                ViewBag.Features = await _context.Features.AsNoTracking().ToListAsync();
                await _cardservice.UpdateAsync(vm, _env.WebRootPath);

                return RedirectToAction(nameof(Index));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Index));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

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

        [Authorize(Roles = "Moderator, Admin")]
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
        [Authorize(Roles = "Moderator, Admin")]
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
        [Authorize(Roles = "Admin")]
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