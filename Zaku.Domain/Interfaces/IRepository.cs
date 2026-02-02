using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Zaku.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
