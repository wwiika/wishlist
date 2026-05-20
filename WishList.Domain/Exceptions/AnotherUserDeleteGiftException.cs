using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class AnotherUserDeleteGiftException(Gift gift, User user)
    : InvalidOperationException($"Пользователь {user.Username.Value} не может удалить подарок '{gift.Title.Value}' пользователя {gift.User.Username.Value} (gift id = {gift.Id}).")
{
    public Gift Gift => gift;
    public User User => user;
}