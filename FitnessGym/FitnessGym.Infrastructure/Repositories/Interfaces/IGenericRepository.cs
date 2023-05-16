using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Filters;
using FluentResults;
using System.Linq.Expressions;

namespace FitnessGym.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IAuditableEntity
    {
        Task<Result> Add(T entity, CancellationToken cancellationToken = default);
        Task<Result> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Result Update(T entityToUpdate);
        Result Delete(T entityToDelete);
        Task<Result<T>> GetById(object entityId, CancellationToken cancellationToken = default);
        Task<List<T>> Get(Expression<Func<T, bool>> filter, PaginationFilter paginationFilter);// de modificat
    }
}
