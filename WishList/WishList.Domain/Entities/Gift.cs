using WishList.Domain.Enums;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList.Domain.Entities;

public class Gift
{
    private static int _giftCounter = 0;
    private readonly List<Reservation> _reservations = new();

    public int Id { get; private set; }
    public int OwnerId { get; private set; }
    public Title Title { get; private set; }
    public Description? Description { get; private set; }
    public string? Link { get; private set; }
    public Price Price { get; private set; }
    public GiftStatus Status { get; private set; }

    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();
    public Reservation? CurrentReservation { get; private set; }

    public bool IsAvailable => Status == GiftStatus.Available;
    public bool IsReserved => Status == GiftStatus.Reserved;
    public bool IsPurchased => Status == GiftStatus.Purchased;

    public Gift(int ownerId, Title title, string? description, string? link, Price price)
    {
        _giftCounter++;
        Id = _giftCounter;
        OwnerId = ownerId;
        Title = title;
        Description = description == null ? null : new Description(description);
        Link = link;
        Price = price;
        Status = GiftStatus.Available;
    }

    public void Update(Title? title, string? description, string? link, Price? price)
    {
        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);
        if (Status == GiftStatus.Reserved)
            throw new GiftAlreadyReservedException(this);
        if (title != null)
            Title = title;
        if (description != null)
            Description = new Description(description);
        if (link != null)
            Link = link;
        if (price != null)
            Price = price;
    }

    public void Reserve(int friendId)
    {
        if (friendId == OwnerId)
            throw new CannotReserveOwnGiftException();

        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status == GiftStatus.Reserved)
            throw new GiftAlreadyReservedException(this);

        var reservation = new Reservation(Id, friendId);
        _reservations.Add(reservation);
        CurrentReservation = reservation;
        Status = GiftStatus.Reserved;
    }

    public void MarkAsPurchased(int buyerId)
    {
        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status != GiftStatus.Reserved)
            throw new InvalidOperationException("Нельзя купить незабронированный подарок");

        if (CurrentReservation?.FriendId != buyerId)
            throw new InvalidOperationException("Купить может только тот, кто забронировал");

        Status = GiftStatus.Purchased;
        CurrentReservation.MarkAsPurchased();
    }

    public void CancelReservation()
    {
        if (Status == GiftStatus.Purchased)
            throw new GiftAlreadyPurchasedException(this);

        if (Status != GiftStatus.Reserved)
            return;

        CurrentReservation?.Cancel();
        CurrentReservation = null;
        Status = GiftStatus.Available;
    }

    public override string ToString()
    {
        string desc = Description?.Value == null ? "" : $" ({Description.Value})";
        return $"Подарок #{Id}: {Title.Value}{desc}, {Price.Value} руб., {Status}";
    }
}