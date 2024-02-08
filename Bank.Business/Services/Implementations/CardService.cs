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

        public async Task CreateAsync(CreateCardVm vm, string env)
        {
            var exists = vm.Title == null || vm.Description == null || vm.ImageUrl == null;

          
            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            Card newCad = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                CategoryId = vm.CategoryId,
                FeaturesIds = vm.FeaturesIds,
                CardImages = new List<CardImage>(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
     
           
            if (!vm.CardHower.CheckImage()) throw new ObjectParamsNullException("File format must be image and size must be lower than 3MB", nameof(vm.CardHower));
            if (!vm.CardPoster.CheckImage()) throw new ObjectParamsNullException("File format must be image and size must be lower than 3MB", nameof(vm.CardPoster));

            newCad.ImageUrl = vm.CardPoster.Upload(env, @"/Upload/CardImages/");
            newCad.ImageUrl = vm.CardHower.Upload(env, @"/Upload/CardImages/");

            await _rep.CreateAsync(newCad);
            await _rep.SaveChangesAsync();
        }

        public Task<List<Currency>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Currency> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
    }
}

   
