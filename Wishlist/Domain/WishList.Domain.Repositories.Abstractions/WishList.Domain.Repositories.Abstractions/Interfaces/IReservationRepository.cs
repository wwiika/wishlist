using WishList.Domain.Entities;
using WishList.Domain.Repositories.Abstractions.Base;
using WishList.ValueObjects;

namespace WishList.Domain.Repositories;

public interface IReservationRepository : IRepository<Reservation, Guid>
{
    Task<IReadOnlyList<Reservation>> GetByFriendIdAsync(UserId friendId, CancellationToken cancellationToken = default);
}