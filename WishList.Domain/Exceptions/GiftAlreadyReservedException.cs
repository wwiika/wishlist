using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class GiftAlreadyReservedException(Gift gift, Friend friend)
    : InvalidOperationException($"Пользователь {friend.Username.Value} не может забронировать подарок '{gift.Title.Value}', так как он уже забронирован (gift id = {gift.Id}, владелец подарка = {gift.User.Username.Value}).")
{
    public Gift Gift => gift;
    public Friend Friend => friend;
}