using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class GiftNotBelongUserException(Gift gift, User user)
    : InvalidOperationException($"Подарок '{gift.Title.Value}' не принадлежит пользователю {user.Username.Value} (gift id = {gift.Id}).")
{
    public Gift Gift => gift;
    public User User => user;
}