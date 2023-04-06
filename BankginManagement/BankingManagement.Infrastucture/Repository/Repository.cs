using BankingManagement.Application.Repositories;
using BankingManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace BankingManagement.Infrastucture.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        #region Private members and CTOR

        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(BankingManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #endregion Private members and CTOR

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                return await _dbSet.ToListAsync(cancellationToken);

            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        //public async Task<PagedList<T>> GetAllByPagingAsync(PagingHelper paging, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        //{
        //    var entities = await _dbSet.Where(predicate).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync(cancellationToken);

        //    var a = PagedList<T>.ToPagedList(entities, _dbSet.Count(), paging.PageNumber, paging.PageSize);
        //    return a;
        //}

        public async Task<List<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.ToListAsync();
        }

        public async Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T>? query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            if (query == null || query.Count() > 1)
                return null;

            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> GetAsync(CancellationToken cancellationToken, params object[] key)
        {
            return await _dbSet.FindAsync(key, cancellationToken);
        }

        public async Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<List<T>> GetAllByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void BeginTransaction(IsolationLevel isolation)
        {
            _context.Database.BeginTransaction(isolation);
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void ChangeStateToModified(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}