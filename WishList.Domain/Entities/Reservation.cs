using WishList.Domain.Base;
using WishList.Domain.Enums;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Reservation : Entity<Guid>
{
    public Gift Gift { get; private set; }
    public Friend Friend { get; private set; }
    public DateTime ReservedAt { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(Gift gift, Friend friend) : base(Guid.NewGuid())
    {
        Gift = gift ?? throw new ArgumentNullValueException(nameof(gift));
        Friend = friend ?? throw new ArgumentNullValueException(nameof(friend));
        ReservedAt = DateTime.UtcNow;
        Status = ReservationStatus.Active;
    }

    protected Reservation() : base() { }

    protected Reservation(Guid id, Gift gift, Friend friend, DateTime reservedAt, ReservationStatus status) : base(id)
    {
        Gift = gift;
        Friend = friend;
        ReservedAt = reservedAt;
        Status = status;
    }

    public bool MarkAsPurchased(Friend buyer)
    {
        if (buyer != Friend)
            throw new AnotherFriendPurchaseReservationException(this, buyer);

        if (Status != ReservationStatus.Active)
            return false;

        Status = ReservationStatus.Purchased;
        Gift.MarkAsPurchasedByReservation();

        return true;
    }

    public bool Cancel(Friend canceller)
    {
        if (canceller != Friend)
            throw new AnotherFriendCancelReservationException(this, canceller);

        if (Status == ReservationStatus.Purchased)
            return false;

        Status = ReservationStatus.Cancelled;
        return true;
    }
}