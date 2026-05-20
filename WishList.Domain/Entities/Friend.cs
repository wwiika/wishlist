using WishList.Domain.Base;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Friend : Entity<Guid>
{
    private readonly ICollection<Reservation> _reservations = new List<Reservation>();

    public UserId FriendId { get; private set; }
    public Username Username { get; private set; }
    public IReadOnlyCollection<Reservation> Reservations => _reservations as IReadOnlyCollection<Reservation> ?? _reservations.ToList().AsReadOnly();

    public Friend(UserId friendId, Username username) : base(Guid.NewGuid())
    {
        FriendId = friendId ?? throw new ArgumentNullValueException(nameof(friendId));
        Username = username ?? throw new ArgumentNullValueException(nameof(username));
    }

    protected Friend() : base() { }

    public Reservation ReserveGift(Gift gift)
    {
        if (gift.User.UserId == FriendId)
            throw new CannotReserveOwnGiftException(gift, this);

        var reservation = gift.Reserve(this);
        _reservations.Add(reservation);
        return reservation;
    }

    public bool PurchaseGift(Reservation reservation)
    {
        if (reservation.Friend != this)
            throw new AnotherFriendPurchaseReservationException(reservation, this);

        if (reservation.Gift.User.UserId == FriendId)
            throw new CannotPurchaseOwnGiftException(reservation.Gift, this);

        return reservation.MarkAsPurchased(this);
    }

    public bool CancelReservation(Reservation reservation)
    {
        if (reservation.Friend != this)
            throw new AnotherFriendCancelReservationException(reservation, this);

        var result = reservation.Cancel(this);
        if (result)
            reservation.Gift.ClearCurrentReservation();

        return result;
    }
}