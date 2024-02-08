using Bank.Core.Entities.Models;
using Bank.DAL.Repositories.Absrtactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories.Interface
{
    public interface ICardRepository:IRepository<Card>
    {
    }
}
