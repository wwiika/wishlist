using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class AnotherUserEditGiftException(Gift gift, User user)
    : InvalidOperationException($"Пользователь {user.Username.Value} не может редактировать подарок '{gift.Title.Value}' пользователя {gift.User.Username.Value} (gift id = {gift.Id}).")
{
    public Gift Gift => gift;
    public User User => user;
}