using Bank.Business.ViewModels.BankIcon;
using Bank.Business.ViewModels.Question;
using Bank.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface IQuestionService
    {
        Task<IQueryable<Question>> GetAllAsync();
        Task<Question> GetByIdAsync(int id);
        Task CreateAsync(QuestionCreateVm vm);
        Task UpdateAsync(QuestionUpdateVm vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
