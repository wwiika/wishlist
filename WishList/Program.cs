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
            Console.WriteLine("---- ПОЛЬЗОВАТЕЛИ ----");
            var ownerId = UserId.New();
            var friendId = UserId.New();
            Console.WriteLine($"Владелец: ID = {ownerId.Value}");
            Console.WriteLine($"Друг: ID = {friendId.Value}");
            Console.WriteLine();

            Console.WriteLine("---- СОЗДАНИЕ ПОДАРКОВ ----");
            var gift1 = new Gift(ownerId, new Title("Белый дом"), new Link("https://example.com/1"), new Price(150000m));
            var gift2 = new Gift(ownerId, new Title("Голубые ставни"), new Link("https://example.com/2"), new Price(2500m));
            var gift3 = new Gift(ownerId, new Title("Мольберт"), new Link("https://example.com/3"), new Price(22345m));
            Console.WriteLine(gift1);
            Console.WriteLine(gift2);
            Console.WriteLine(gift3);
            Console.WriteLine();

            Console.WriteLine("---- БРОНИРОВАНИЕ ----");
            var reservation = gift1.Reserve(friendId);
            Console.WriteLine(gift1);
            Console.WriteLine($"Бронь создана: ID = {reservation.Id}");
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: повторное бронирование ----");
            try
            {
                gift1.Reserve(friendId);
            }
            catch (GiftAlreadyReservedException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: бронь своего подарка ----");
            try
            {
                gift2.Reserve(ownerId);
            }
            catch (CannotReserveOwnGiftException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- ПОКУПКА ----");
            var success = gift1.MarkAsPurchased(friendId);
            Console.WriteLine(success ? gift1.ToString() : "Ошибка покупки");
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: редактирование купленного ----");
            var updated = gift1.Update(new Title("Голубой дом"), new Link("https://example.com/new"), new Price(126789m));
            Console.WriteLine(updated ? "Обновлено" : "Нельзя обновить купленный подарок");
            Console.WriteLine();

            Console.WriteLine("---- РЕДАКТИРОВАНИЕ ДОСТУПНОГО ----");
            gift2.Update(new Title("Желтые ставни"), new Link("https://example.com/new2"), new Price(2345m));
            Console.WriteLine(gift2);
            Console.WriteLine();

            Console.WriteLine("---- ОТМЕНА БРОНИ ----");
            gift3.Reserve(friendId);
            Console.WriteLine($"До отмены: {gift3}");
            var cancelled = gift3.CancelReservation();
            Console.WriteLine(cancelled ? "Бронь отменена" : "Не удалось отменить бронь");
            Console.WriteLine($"После отмены: {gift3}");
            Console.WriteLine();

            Console.WriteLine("---- СТАТУСЫ ПОДАРКОВ ----");
            Console.WriteLine(gift1);
            Console.WriteLine(gift2);
            Console.WriteLine(gift3);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}