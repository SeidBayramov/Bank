
using Bank.Business.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Interface
{
    public interface ICardRequestService
    {
        Task Apply(CardRequestVm vm);
    }
}
