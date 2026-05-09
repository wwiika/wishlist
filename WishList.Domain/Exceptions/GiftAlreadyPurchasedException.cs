using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class GiftAlreadyPurchasedException : InvalidOperationException
{
    public GiftAlreadyPurchasedException(Gift gift)
        : base($"Подарок '{gift.Title.Value}' уже куплен")
    {
    }

    public GiftAlreadyPurchasedException() : base("Подарок уже куплен")
    {
    }
}