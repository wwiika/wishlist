using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class GiftAlreadyReservedException : InvalidOperationException
{
    public GiftAlreadyReservedException(Gift gift)
        : base($"Подарок '{gift.Title}' уже забронирован")
    {
    }

    public GiftAlreadyReservedException() : base("Подарок уже забронирован")
    {
    }
}