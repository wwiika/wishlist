using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class AnotherFriendPurchaseReservationException(Reservation reservation, Friend friend)
    : InvalidOperationException($"Пользователь {friend.Username.Value} не может купить бронь, созданную другим другом (reservation id = {reservation.Id}, владелец брони = {reservation.Friend.Username.Value}).")
{
    public Reservation Reservation => reservation;
    public Friend Friend => friend;
}