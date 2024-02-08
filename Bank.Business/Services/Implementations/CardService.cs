using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _rep;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFeatureRepository _featureRepository;

        public CardService(ICardRepository rep, IWebHostEnvironment env, ICategoryRepository categoryRepository, IFeatureRepository featureRepository)
        {
            _rep = rep;
            _env = env;
            _categoryRepository = categoryRepository;
            _featureRepository = featureRepository;
        }
        private string[] includes = {
            "Category",
            "CardFeatures", "CardFeatures.Feature",
            "CardImages",
        };

        public async Task<IQueryable<Card>> GetAllAsync()
        {
            IQueryable<Card> query = await _rep.GetAllAsync(includes: includes);
            return query;
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            var oldcard = await CheckProduct(id, includes);
            return oldcard;
        }

        public async Task CreateAsync(CreateCardVm vm, string env)
        {
            var exists = vm.Title == null || vm.Description == null;/* vm.ImageUrl == null;*/

          
            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            Card newCad = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                CategoryId = vm.CategoryId,
                CardImages = new List<CardImage>(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
     
           
        
            //newCad.ImageUrl = vm.CardPoster.Upload(env, @"/Upload/CardImages/");
            //newCad.ImageUrl = vm.CardHower.Upload(env, @"/Upload/CardImages/");

            await _rep.CreateAsync(newCad);
            await _rep.SaveChangesAsync();
        }

     

        public async Task DeleteAsync(int id)
        {
            await CheckService(id);

            await _rep.DeleteAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task RecoverAsync(int id)
        {
            await CheckService(id);

            await _rep.RecoverAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await CheckService(id);

            await _rep.RemoveAsync(id);
            await _rep.SaveChangesAsync();
        }
        public async Task<Card> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldIcon = await _rep.GetByIdAsync(id);
            if (oldIcon == null) throw new ObjectNullException("There is no object like in data!", nameof(oldIcon));

            return oldIcon;
        }

        public Task UpdateAsync(UpdateCardVm vm, string env)
        {
            throw new NotImplementedException();
        }
        public async Task<Card> CheckProduct(int id, params string[] includes)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be over than and not equal to zero!", nameof(id));
            var oldProduct = await _rep.GetByIdAsync(Id: id, entityIncludes: includes);
            if (oldProduct is null) throw new ObjectNullException("There is no like that object in Data!", nameof(oldProduct));

            return oldProduct;
        }
    }
}

   
