using WishList.Domain.Entities;
using WishList.Domain.Exceptions;
using WishList.ValueObjects;

namespace WishList;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("---- СОЗДАНИЕ ПОЛЬЗОВАТЕЛЯ И ДРУГА ----");
            var user = new User(UserId.New(), new Username("Лариса Долина"));
            var friend = new Friend(UserId.New(), new Username("Тимати"));
            Console.WriteLine($"Пользователь: {user.Username.Value}, ID = {user.UserId.Value}");
            Console.WriteLine($"Друг: {friend.Username.Value}, ID = {friend.FriendId.Value}");
            Console.WriteLine();

            Console.WriteLine("---- ПОЛЬЗОВАТЕЛЬ СОЗДАЁТ ПОДАРКИ ----");
            var gift1 = user.CreateGift(new Title("Белый дом"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(150000m));
            var gift2 = user.CreateGift(new Title("Голубые ставни"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(2500m));
            var gift3 = user.CreateGift(new Title("Мольберт"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(22345m));
            Console.WriteLine(gift1);
            Console.WriteLine(gift2);
            Console.WriteLine(gift3);
            Console.WriteLine();

            Console.WriteLine("---- ДРУГ БРОНИРУЕТ ПОДАРОК ----");
            var reservation = friend.ReserveGift(gift1);
            Console.WriteLine(gift1);
            Console.WriteLine($"Бронь создана: ID = {reservation.Id}");
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: повторное бронирование ----");
            try
            {
                friend.ReserveGift(gift1);
            }
            catch (GiftAlreadyReservedException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: бронь своего подарка ----");
            try
            {
                var thisUser = new Friend(user.UserId, new Username("Билан"));
                thisUser.ReserveGift(gift2);
            }
            catch (CannotReserveOwnGiftException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ПОЛЬЗОВАТЕЛЬ МЕНЯЕТ СТАТУС ПОДАРКА ----");
            var gift4 = user.CreateGift(new Title("Кисточки"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(1000000m));
            user.MarkGiftAsPurchased(gift4);
            Console.WriteLine(gift4);
            Console.WriteLine();

            Console.WriteLine("---- ДРУГ ПОКУПАЕТ ПОДАРОК ----");
            friend.PurchaseGift(reservation);
            Console.WriteLine(gift1);
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: редактирование чужого подарка ----");
            try
            {
                var otherUser = new User(UserId.New(), new Username("Лазарев"));
                otherUser.EditGift(gift1, new Title("Микрофон"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(1000m));
            }
            catch (AnotherUserEditGiftException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: удаление чужого подарка ----");
            try
            {
                var otherUser = new User(UserId.New(), new Username("Сережа Лазарев"));
                otherUser.DeleteGift(gift1);
            }
            catch (AnotherUserDeleteGiftException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: друг покупает чужую бронь ----");
            try
            {
                var otherFriend = new Friend(UserId.New(), new Username("Пугачева"));
                otherFriend.PurchaseGift(reservation);
            }
            catch (AnotherFriendPurchaseReservationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: друг отменяет чужую бронь ----");
            try
            {
                var otherFriend = new Friend(UserId.New(), new Username("Олечка Бузова"));
                otherFriend.CancelReservation(reservation);
            }
            catch (AnotherFriendCancelReservationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- РЕДАКТИРОВАНИЕ ДОСТУПНОГО ПОДАРКА ----");
            user.EditGift(gift2, new Title("Желтые ставни"), new Link("https://youtu.be/kvjShHlG-nc?list=RDRyLc6Sa7L6U"), new Price(2345m));
            Console.WriteLine(gift2);
            Console.WriteLine();

            Console.WriteLine("---- ОТМЕНА БРОНИ ----");
            var reservation3 = friend.ReserveGift(gift3);
            Console.WriteLine($"До отмены: {gift3}");
            friend.CancelReservation(reservation3);
            Console.WriteLine($"После отмены: {gift3}");
            Console.WriteLine();

            Console.WriteLine("---- СТАТУСЫ ПОДАРКОВ ----");
            Console.WriteLine(gift1);
            Console.WriteLine(gift2);
            Console.WriteLine(gift3);
            Console.WriteLine(gift4);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}