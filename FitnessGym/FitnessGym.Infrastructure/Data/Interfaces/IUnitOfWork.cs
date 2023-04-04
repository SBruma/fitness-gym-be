namespace FitnessGym.Infrastructure.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}
