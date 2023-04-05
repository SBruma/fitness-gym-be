using FitnessGym.Domain.Entities.Interfaces;

namespace FitnessGym.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, IAuditableEntity
    {
        Task Add(T entity, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Update(T entityToUpdate);
        void Delete(T entityToDelete);
        Task<T?> GetById(object entityId, CancellationToken cancellationToken = default);
    }
}
