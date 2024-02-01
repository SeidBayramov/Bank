using Bank.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories.Interface
{
    public interface IRepository<T> where T : BaseAudiTable, new()
    {
        Task<IQueryable<T>> GetAllAsync(
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           params Expression<Func<T, object>>[] includes
       );
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        Task<T> RecoverAsync(int id);
        Task RemoveAsync(int id);
        Task<int> SaveChangesAsync();

    }
}
