namespace WishList.Domain.Repositories.Abstractions.Base;

public interface IRepository<TEntity, in TId>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken cancellationToken = default, bool asNoTracking = false);

    Task<TEntity?> AddAsync( TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync( TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync( TId id, CancellationToken cancellationToken = default);
}