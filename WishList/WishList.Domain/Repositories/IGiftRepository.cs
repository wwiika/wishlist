using WishList.Domain.Entities;

namespace WishList.Domain.Repositories;

public interface IGiftRepository
{
    Task<Gift?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Gift>> GetByOwnerIdAsync(int ownerId, CancellationToken cancellationToken = default);
    Task AddAsync(Gift gift, CancellationToken cancellationToken = default);
    Task UpdateAsync(Gift gift, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}