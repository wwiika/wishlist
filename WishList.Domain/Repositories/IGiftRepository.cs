using WishList.Domain.Entities;
using WishList.Domain.Repositories.Abstractions;
using WishList.ValueObjects;

namespace WishList.Domain.Repositories;

public interface IGiftRepository : IRepository<Gift, Guid>
{
    Task<IReadOnlyList<Gift>> GetByOwnerIdAsync(UserId ownerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Gift>> GetAvailableGiftsAsync(CancellationToken cancellationToken = default);
}