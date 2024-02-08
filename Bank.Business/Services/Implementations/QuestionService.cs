using Bank.Business.Exceptions.Common;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Question;
using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionsRepository _rep;

        public QuestionService(IQuestionsRepository rep)
        {
            _rep = rep;
        }

        public async Task<IQueryable<Question>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }


        public async Task<Question> GetByIdAsync(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(id));

            return await _rep.GetByIdAsync(id);
        }

        public async Task CreateAsync(QuestionCreateVm vm)
        {
            var exists = vm.Title == null || vm.Description == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            Question question = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            await _rep.CreateAsync(question);
            await _rep.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionUpdateVm vm)
        {
            if (vm.Id <= 0) throw new IdNegativeOrZeroException("Id must be positive and over than zero!", nameof(vm.Id));

            Question oldquestion = await _rep.GetByIdAsync(vm.Id);

            if (oldquestion is null) throw new ObjectNullException("There is no blog in data!", nameof(oldquestion));

            if (vm.Title is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Title));
            }
            if (vm.Description is null)
            {
                throw new ObjectNullException("Object is required!", nameof(vm.Description));
            }


            oldquestion.Title = vm.Title;
            oldquestion.Description = vm.Description;
            oldquestion.UpdatedDate = DateTime.Now;

            await _rep.UpdateAsync(oldquestion);
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
        public async Task<Question> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldIcon = await _rep.GetByIdAsync(id);
            if (oldIcon == null) throw new ObjectNullException("There is no object like in data!", nameof(oldIcon));

            return oldIcon;
        }
    }
}
