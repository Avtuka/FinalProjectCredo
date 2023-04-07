using System.Data;
using System.Linq.Expressions;

namespace BankingManagement.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

        //Task<PagedList<T>> GetAllByPagingAsync(PagingHelper paging, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<T> GetAsync(CancellationToken cancellationToken, params object[] key);

        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task<List<T>> GetAllWithIncludeAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        Task<T> GetWithIncludeAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        Task<List<T>> GetAllByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);

        void BeginTransaction(IsolationLevel isolation);

        void CommitTransaction();

        void ChangeStateToModified(T entity);

        void RollbackTransaction();

        void Delete(T entity);
    }
}