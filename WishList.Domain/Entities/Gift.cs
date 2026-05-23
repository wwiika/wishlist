using WishList.Domain.Base;
using WishList.Domain.Enums;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Gift : Entity<Guid>
{
    private readonly ICollection<Reservation> _reservations = new List<Reservation>();

    public User User { get; private set; }
    public Title Title { get; private set; }
    public Link? Link { get; private set; }
    public Price Price { get; private set; }
    public GiftStatus Status { get; private set; }
    public IReadOnlyCollection<Reservation> Reservations => _reservations as IReadOnlyCollection<Reservation> ?? _reservations.ToList().AsReadOnly();
    public Reservation? CurrentReservation { get; private set; }

    public Gift(User user, Title title, Link? link, Price price) : base(Guid.NewGuid())
    {
        User = user ?? throw new ArgumentNullValueException(nameof(user));
        Title = title ?? throw new ArgumentNullValueException(nameof(title));
        Link = link;
        Price = price ?? throw new ArgumentNullValueException(nameof(price));
        Status = GiftStatus.Available;
    }

    protected Gift() : base() { }

    protected Gift(Guid id, User user, Title title, Link? link, Price price, GiftStatus status) : base(id)
    {
        User = user;
        Title = title;
        Link = link;
        Price = price;
        Status = status;
    }

    public bool Update(Title? title, Link? link, Price? price)
    {
        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status == GiftStatus.Reserved)
            throw new GiftAlreadyReservedException(this, null!);

        bool changed = false;
        if (title != null) { Title = title; changed = true; }
        if (link != null) { Link = link; changed = true; }
        if (price != null) { Price = price; changed = true; }

        return changed;
    }

    public Reservation Reserve(Friend friend)
    {
        if (friend is null)
            throw new ArgumentNullValueException(nameof(friend));

        if (User.UserId == friend.FriendId)
            throw new CannotReserveOwnGiftException(this, friend);

        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status == GiftStatus.Reserved)
            throw new GiftAlreadyReservedException(this,friend);

        var reservation = new Reservation(this, friend);
        _reservations.Add(reservation);
        CurrentReservation = reservation;
        Status = GiftStatus.Reserved;

        return reservation;
    }

    public bool MarkAsPurchasedByOwner()
    {
        if (Status == GiftStatus.Purchased)
            return false;

        if (Status == GiftStatus.Reserved)
        {
            if (CurrentReservation?.Friend != null)
                throw new InvalidOperationException($"Подарок уже забронирован пользователем {CurrentReservation.Friend.Username.Value}");
        }

        Status = GiftStatus.Purchased;
        return true;
    }

    public void MarkAsPurchasedByReservation()
    {
        if (Status == GiftStatus.Purchased)
            return;

        if (Status != GiftStatus.Reserved)
            throw new InvalidOperationException($"Нельзя купить подарок в статусе {Status}");

        Status = GiftStatus.Purchased;
    }

    public void ClearCurrentReservation()
    {
        if (CurrentReservation != null && CurrentReservation.Status != ReservationStatus.Active)
        {
            CurrentReservation = null;
            Status = GiftStatus.Available;
        }
    }

    public override string ToString()
    {
        return $"Подарок: {Title.Value}, {Price.Value} руб., {Status}";
    }
}