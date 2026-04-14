using WishList.Domain.Enums;

namespace WishList.Domain.Entities;

public class Reservation
{
    private static int _reservationCounter = 0;

    public int Id { get; private set; }
    public int GiftId { get; private set; }
    public int FriendId { get; private set; }
    public DateTime ReservedAt { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Reservation(int giftId, int friendId)
    {
        _reservationCounter++;
        Id = _reservationCounter;
        GiftId = giftId;
        FriendId = friendId;
        ReservedAt = DateTime.UtcNow;
        Status = ReservationStatus.Active;
    }

    public void MarkAsPurchased()
    {
        if (Status != ReservationStatus.Active)
            throw new InvalidOperationException($"Бронь в статусе {Status}");

        Status = ReservationStatus.Purchased;
    }

    public void Cancel()
    {
        if (Status == ReservationStatus.Purchased)
            throw new InvalidOperationException("Нельзя отменить купленную бронь");

        Status = ReservationStatus.Cancelled;
    }
}