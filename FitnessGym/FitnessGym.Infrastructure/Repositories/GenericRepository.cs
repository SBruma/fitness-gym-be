using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAuditableEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task Add(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Delete(T entityToDelete)
        {
            _dbSet.Attach(entityToDelete);
            _context.Entry(entityToDelete).State = EntityState.Modified;
            entityToDelete.IsDeleted = true;
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);
        }

        public virtual async Task<T?> GetById(object entityId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { entityId }, cancellationToken);
        }
    }
}
