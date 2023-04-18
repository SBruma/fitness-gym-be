using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Filters;
using System.Linq.Expressions;

namespace FitnessGym.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IAuditableEntity
    {
        Task Add(T entity, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Update(T entityToUpdate);
        void Delete(T entityToDelete);
        Task<T?> GetById(object entityId, CancellationToken cancellationToken = default);
        Task<List<T>> Get(Expression<Func<T, bool>> filter, PaginationFilter paginationFilter);
    }
}
