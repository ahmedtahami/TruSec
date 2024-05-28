using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruSec.DAL.Repositories
{
    using global::TruSec.DAL.DbContexts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    namespace TruSec.DAL.Repositories
    {
        public class Repository<T> : IRepository<T> where T : class
        {
            protected readonly ApplicationDbContext _context;
            private readonly DbSet<T> _entities;

            public Repository(ApplicationDbContext context)
            {
                _context = context;
                _entities = _context.Set<T>();
            }

            public async Task<T> GetByIdAsync(int id)
            {
                return await _entities.FindAsync(id);
            }

            public async Task<IQueryable<T>> GetAllAsync(bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _context.Set<T>().AsNoTracking()) : await Task.Run(() => _context.Set<T>());

            public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
                !trackChanges ? await Task.Run(() => _context.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _context.Set<T>().Where(expression));

            public async Task AddAsync(T entity)
            {
                await _entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task AddRangeAsync(IEnumerable<T> entities)
            {
                await _entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }

            public void Remove(T entity)
            {
                _entities.Remove(entity);
                _context.SaveChanges();
            }

            public void RemoveRange(IEnumerable<T> entities)
            {
                _entities.RemoveRange(entities);
                _context.SaveChanges();
            }
        }
    }

}
