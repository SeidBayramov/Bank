using Bank.Core.Entities.Commons;
using Bank.DAL.Context;
using Bank.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories.Absrtactions
{
    public class Repository<T> : IRepository<T> where T : BaseAudiTable, new()
    {
        private readonly AppDbContext _context;
        public  DbSet<T> Table=>_context.Set<T>();

        public Repository(AppDbContext context)
        {
            _context = context;
         
        }

        public async Task<T> CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            entity.IsDeleted = true;
            await UpdateAsync(entity);
            return entity;
        }
        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync(); 
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> RecoverAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            entity.IsDeleted = false;
            await UpdateAsync(entity);

            return entity;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            Table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Table.Update(entity);
            return entity;
        }
    }
}
