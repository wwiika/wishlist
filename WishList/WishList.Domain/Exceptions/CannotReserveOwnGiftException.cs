namespace WishList.Domain.Exceptions;

public class CannotReserveOwnGiftException : InvalidOperationException
{
    public CannotReserveOwnGiftException()
        : base("Нельзя забронировать свой подарок")
    {
    }
}