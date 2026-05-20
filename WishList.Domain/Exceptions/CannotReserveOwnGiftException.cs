using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class CannotReserveOwnGiftException(Gift gift, Friend friend)
    : InvalidOperationException($"Пользователь {friend.Username.Value} не может забронировать свой собственный подарок '{gift.Title.Value}' (gift id = {gift.Id}).")
{
    public Gift Gift => gift;
    public Friend Friend => friend;
}