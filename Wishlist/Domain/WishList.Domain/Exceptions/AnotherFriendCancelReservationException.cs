using WishList.Domain.Entities;

namespace WishList.Domain.Exceptions;

public class AnotherFriendCancelReservationException(Reservation reservation, Friend friend)
    : InvalidOperationException($"Пользователь {friend.Username.Value} не может отменить бронь, созданную другим другом (reservation id = {reservation.Id}, владелец брони = {reservation.Friend.Username.Value}).")
{
    public Reservation Reservation => reservation;
    public Friend Friend => friend;
}