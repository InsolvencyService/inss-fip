using Microsoft.EntityFrameworkCore;

namespace INSS.FIP.Interfaces;

public interface IDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
