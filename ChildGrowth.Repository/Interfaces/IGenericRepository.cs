using System.Linq.Expressions;
using ChildGrowth.Domain.Filter;
using ChildGrowth.Domain.Paginate;
using Microsoft.EntityFrameworkCore.Query;

namespace ChildGrowth.Repository.Interfaces;

public interface IGenericRepository<T> : IDisposable where T : class
{
    #region Get Async

    Task<T> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<TResult> SingleOrDefaultAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<ICollection<T>> GetListAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<ICollection<TResult>> GetListAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

    Task<IPaginate<T>> GetPagingListAsync(
        Expression<Func<T, bool>> predicate = null,
        IFilter<T> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int page = 1,
        int size = 10,
        string sortBy = null,
        bool isAsc = true);

    // Task<IPaginate<TResult>> GetPagingListAsync<TResult>(
    //     Expression<Func<T, TResult>> selector,
    //     Expression<Func<T, bool>> predicate = null,
    //     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
    //     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
    //     int page = 1,
    //     int size = 10);

    #endregion

    #region Insert

    Task InsertAsync(T entity);

    Task InsertRangeAsync(IEnumerable<T> entities);

    #endregion

    #region Update

    void UpdateAsync(T entity);

    void UpdateRange(IEnumerable<T> entities);

    #endregion

    void DeleteAsync(T entity);
    void DeleteRangeAsync(IEnumerable<T> entities);
}