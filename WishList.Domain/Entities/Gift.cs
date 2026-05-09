using WishList.Domain.Base;
using WishList.Domain.Enums;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Gift : Entity<Guid>
{
    private readonly List<Reservation> _reservations = new();

    public UserId OwnerId { get; private set; }
    public Title Title { get; private set; }
    public Link? Link { get; private set; }
    public Price Price { get; private set; }
    public GiftStatus Status { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    public Reservation? CurrentReservation { get; private set; }

    public bool IsAvailable => Status == GiftStatus.Available;
    public bool IsReserved => Status == GiftStatus.Reserved;
    public bool IsPurchased => Status == GiftStatus.Purchased;

    public Gift(UserId ownerId, Title title, Link? link, Price price) : base(Guid.NewGuid())
    {
        OwnerId = ownerId;
        Title = title;
        Link = link;
        Price = price;
        Status = GiftStatus.Available;
    }

    public bool Update(Title? title, Link? link, Price? price)
    {
        if (Status == GiftStatus.Purchased || Status == GiftStatus.Reserved)
            return false;

        if (title != null) Title = title;
        if (link != null) Link = link;
        if (price != null) Price = price;

        return true;
    }

    public Reservation Reserve(UserId friendId)
    {
        if (friendId == OwnerId)
            throw new CannotReserveOwnGiftException();

        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status == GiftStatus.Reserved)
            throw new GiftAlreadyReservedException(this);

        var reservation = new Reservation(this, friendId);
        _reservations.Add(reservation);
        CurrentReservation = reservation;
        Status = GiftStatus.Reserved;

        return reservation;
    }

    public bool MarkAsPurchased(UserId buyerId)
    {
        if (Status != GiftStatus.Reserved)
            return false;

        if (CurrentReservation?.FriendId != buyerId)
            return false;

        Status = GiftStatus.Purchased;
        CurrentReservation.MarkAsPurchased();
        return true;
    }

    public bool CancelReservation()
    {
        if (Status != GiftStatus.Reserved)
            return false;

        CurrentReservation?.Cancel();
        CurrentReservation = null;
        Status = GiftStatus.Available;
        return true;
    }

    public override string ToString()
    {
        return $"Подарок: {Title.Value}, {Price.Value} руб., {Status}";
    }
}