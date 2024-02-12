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
        public DbSet<T> Table;

        public Repository(AppDbContext context)
        {
            _context = context;
            Table = _context.Set<T>();
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
        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? expressionOrder = null,
        bool isDescending = false,
        params string[] includes)
        {
            IQueryable<T> query = Table;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (expressionOrder is not null)
            {
                query = isDescending ? query.OrderByDescending(expressionOrder) : query.OrderBy(expressionOrder);
            }

            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int Id = 0, params string[] entityIncludes)
        {
            IQueryable<T> query = Table;

            if (entityIncludes is not null)
            {
                for (int i = 0; i < entityIncludes.Length; i++)
                {
                    query = query.Include(entityIncludes[i]);
                }
            }

            return await query.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
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
