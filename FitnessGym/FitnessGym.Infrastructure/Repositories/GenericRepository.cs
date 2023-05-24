using FitnessGym.Domain.Entities.Interfaces;
using FitnessGym.Domain.Filters;
using FitnessGym.Infrastructure.Data;
using FitnessGym.Infrastructure.Errors;
using FitnessGym.Infrastructure.Repositories.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public virtual async Task<Result> Add(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbSet.AddAsync(entity, cancellationToken);
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail(new NotCreatedError(entity.GetType()));
            }
        }

        public virtual async Task<Result> AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities, cancellationToken);
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail(new NotCreatedError(entities.GetType()));
            }
        }

        public virtual Result Delete(T entityToDelete)
        {
            try
            {
                _dbSet.Attach(entityToDelete);
                _context.Entry(entityToDelete).State = EntityState.Modified;
                entityToDelete.IsDeleted = true;
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail(new NotModifiedError(entityToDelete.GetType()));
            }
        }

        public virtual Result Update(T entityToUpdate)
        {
            try
            {
                _dbSet.Update(entityToUpdate);
                return Result.Ok();
            }
            catch (Exception)
            {
                return Result.Fail(new NotModifiedError(entityToUpdate.GetType()));
            }
        }

        public virtual async Task<Result<T>> GetById(object entityId, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(new object[] { entityId }, cancellationToken);

            return entity is not null ? Result.Ok(entity) : Result.Fail(new NotFoundError(typeof(T)));
        }

        protected IQueryable<T> Get(Expression<Func<T, bool>> filter, PaginationFilter paginationFilter, Expression<Func<T, object>> orderBy = null)
        {
            var query = _dbSet.AsNoTracking().AsQueryable();
            query = query.Where(entity => entity.IsDeleted == false);
            query = filter != null ? query.Where(filter) : query;
            query = orderBy != null ? query.OrderBy(orderBy) : query;
            query = paginationFilter != null ? query.Skip(paginationFilter.Offset).Take(paginationFilter.PageSize) : query;

            return query;
        }
    }
}
