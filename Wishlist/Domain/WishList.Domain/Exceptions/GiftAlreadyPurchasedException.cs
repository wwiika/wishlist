using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class GiftAlreadyPurchasedException(Gift gift, Friend? friend = null)
    : InvalidOperationException($"{(friend != null ? $"Пользователь {friend.Username.Value} не может " : "")}Подарок '{gift.Title.Value}' уже куплен (gift id = {gift.Id}, владелец подарка = {gift.User.Username.Value}).")
{
    public Gift Gift => gift;
    public Friend? Friend => friend;
}