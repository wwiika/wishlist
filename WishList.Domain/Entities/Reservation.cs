using WishList.Domain.Base;
using WishList.Domain.Enums;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Reservation : Entity<Guid>
{
    public Gift Gift { get; private set; }
    public UserId FriendId { get; private set; }
    public DateTime ReservedAt { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(Gift gift, UserId friendId) : base(Guid.NewGuid())
    {
        Gift = gift;
        FriendId = friendId;
        ReservedAt = DateTime.UtcNow;
        Status = ReservationStatus.Active;
    }

    private Reservation(Guid id, Gift gift, UserId friendId, DateTime reservedAt, ReservationStatus status) : base(id)
    {
        Gift = gift;
        FriendId = friendId;
        ReservedAt = reservedAt;
        Status = status;
    }

    public bool MarkAsPurchased()
    {
        if (Status != ReservationStatus.Active)
            return false;

        Status = ReservationStatus.Purchased;
        return true;
    }

    public bool Cancel()
    {
        if (Status == ReservationStatus.Purchased)
            return false;

        Status = ReservationStatus.Cancelled;
        return true;
    }
}