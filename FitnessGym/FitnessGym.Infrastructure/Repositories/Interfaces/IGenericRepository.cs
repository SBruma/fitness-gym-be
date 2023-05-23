using FitnessGym.Domain.Entities.Interfaces;
using FluentResults;

namespace FitnessGym.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IAuditableEntity
    {
        Task<Result> Add(T entity, CancellationToken cancellationToken = default);
        Task<Result> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Result Update(T entityToUpdate);
        Result Delete(T entityToDelete);
        Task<Result<T>> GetById(object entityId, CancellationToken cancellationToken = default);
    }
}
