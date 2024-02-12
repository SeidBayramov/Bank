using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Card;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

            var exists = vm.Title == null || vm.Description == null || vm.CategoryId == null ||
                vm.FeaturesIds == null || vm.CardFiles == null;
          
            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            var existsSameCardTitle = await (await _rep.GetAllAsync(expression: x => x.Title == vm.Title))
                .FirstOrDefaultAsync() is null;
            var existsSameCardDes = await (await _rep.GetAllAsync(expression: x => x.Description == vm.Description))
             .FirstOrDefaultAsync() is null;

            if (!existsSameCardTitle) throw new ObjectSameParamsException("There is same title product in data!", nameof(vm.Title));
            if (!existsSameCardDes) throw new ObjectSameParamsException("There is same title product in data!", nameof(vm.Description));

            Card newCad = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                IsInStock = vm.IsInStock,
                CategoryId = vm.CategoryId,
                CardImages = new List<CardImage>(),
                CardFeatures = new List<CardFeature>(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            if(vm.CardFiles is not null)
            {
                foreach (var item in vm.CardFiles)
                {
                    if(!item.CheckImage()) throw new ImageException("File must be image format and lower than 3MB!", nameof(item));

                    CardImage cardImage = new()
                    {
                        Card = newCad,
                        ImageUrl = item.Upload(env, @"/Upload/CardImages/")
                    };
                    newCad.CardImages.Add(cardImage);
                }
            }
            else
            {
                throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.CardFiles));
            }
     
            if(vm.FeaturesIds is not null||vm.FeaturesIds.Any() )
            {
                foreach (var item in vm.FeaturesIds)
                {
                    CardFeature cardFeature = new()
                    {
                        Card = newCad,
                        FeatureId = item
                    };
                    newCad.CardFeatures.Add(cardFeature);
                }
            }
            else
            {
                throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.CardFiles));
            }

            await _rep.CreateAsync(newCad);
            await _rep.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateCardVm vm, string env)
        {
            var oldcard = await CheckProduct(vm.Id, includes);
            var exists = vm.Title == null || vm.Description == null || vm.CategoryId == null ||
                 vm.FeaturesIds == null || vm.CardFiles == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            var existsSameCardTitle = await (await _rep.GetAllAsync(expression: x => x.Title == vm.Title))
                .FirstOrDefaultAsync() is null;
            var existsSameCardDes = await (await _rep.GetAllAsync(expression: x => x.Description == vm.Description))
             .FirstOrDefaultAsync() is null;

            if (!existsSameCardTitle) throw new ObjectSameParamsException("There is same title product in data!", nameof(vm.Title));
            if (!existsSameCardDes) throw new ObjectSameParamsException("There is same title product in data!", nameof(vm.Description));



            oldcard.Title = vm.Title;
            oldcard.Description = vm.Description;
            oldcard.IsInStock = vm.IsInStock;
            oldcard.UpdatedDate = DateTime.Now;
            oldcard.Category= await _categoryRepository.GetByIdAsync(vm.CategoryId);

            await _rep.UpdateAsync(oldcard);
            oldcard.CardFeatures.Clear();

            if (vm.FeaturesIds is not null || vm.FeaturesIds.Any() )
            {
                foreach (var item in vm.FeaturesIds)
                {
                    CardFeature cardFeature = new()
                    {
                        Card = oldcard,
                        FeatureId = item
                    };
                    oldcard.CardFeatures.Add(cardFeature);
                }
            }
            if (vm.CardFiles == null)
            {
                oldcard.CardImages.Clear();
            }
            else
            {
                List<CardImage> remove = oldcard.CardImages.Where(x => !vm.CardImageIds.Contains(x.Id)).ToList();
                if (remove.Count > 0)
                {
                    foreach (var item in remove)
                    {

                        oldcard.CardImages.Remove(item);
                        FileManager.Delete(item.ImageUrl, env, @"/Upload/CardImages/");
                    }
                }

            }


            if(vm.CardFiles != null)
            {
                foreach (var item in vm.CardFiles)
                {
                    if (!item.CheckImage()) throw new ImageException("File must be image format and lower than 3MB!", nameof(item));

                    CardImage cardImage = new()
                    {
                        Card = oldcard,
                        ImageUrl = item.Upload(env, @"/Upload/CardImages/")
                    };
                    oldcard.CardImages.Add(cardImage);
                }
            }
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

        
        public async Task<Card> CheckProduct(int id, params string[] includes)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be over than and not equal to zero!", nameof(id));
            var oldProduct = await _rep.GetByIdAsync(Id: id, entityIncludes: includes);

            if (oldProduct is null) throw new ObjectNullException("There is no like that object in Data!", nameof(oldProduct));

            return oldProduct;
        }
    }
}

   
