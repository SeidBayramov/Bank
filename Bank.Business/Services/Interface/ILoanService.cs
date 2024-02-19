using Bank.Business.ViewModels.Loan;
using Bank.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface ILoanService
    {
        Task Send(LoanVm vm);
    }
}
