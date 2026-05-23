using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class CannotPurchaseOwnGiftException(Gift gift, Friend friend)
    : InvalidOperationException($"Пользователь {friend.Username.Value} не может купить свой собственный подарок '{gift.Title.Value}' (gift id = {gift.Id}).")
{
    public Gift Gift => gift;
    public Friend Friend => friend;
}