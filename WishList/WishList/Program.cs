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
            var ownerId = 1;
            var friendId = 2;
            Console.WriteLine($"Владелец: ID = {ownerId}");
            Console.WriteLine($"Друг: ID = {friendId}");
            Console.WriteLine();

            Console.WriteLine("---- СОЗДАНИЕ ПОДАРКОВ ----");
            var gift1 = new Gift(ownerId, new Title("Белый дом"), "Недвижимость", "https://rutube.ru/video/c6cc4d620b1d4338901770a44b3e82f4/", new Price(150004500m));
            var gift2 = new Gift(ownerId, new Title("Голубые ставни"), "Интерьер", "https://rutube.ru/video/c6cc4d620b1d4338901770a44b3e82f4/", new Price(2500m));
            var gift3 = new Gift(ownerId, new Title("Мольберт"), "Искусство", "https://rutube.ru/video/c6cc4d620b1d4338901770a44b3e82f4/", new Price(22345m));
            Console.WriteLine(gift1);
            Console.WriteLine(gift2);
            Console.WriteLine(gift3);
            Console.WriteLine();

            Console.WriteLine("---- БРОНИРОВАНИЕ ----");
            gift1.Reserve(friendId);
            Console.WriteLine(gift1);
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
            gift1.MarkAsPurchased(friendId);
            Console.WriteLine(gift1);
            Console.WriteLine();

            Console.WriteLine("---- ОШИБКА: редактирование купленного ----");
            try
            {
                gift1.Update(new Title("Голубой дом"), "Другой цвет", "https://rutube.ru/video/c6cc4d620b1d4338901770a44b3e82f4/", new Price(123456789m));
            }
            catch (GiftAlreadyPurchasedException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("---- РЕДАКТИРОВАНИЕ ДОСТУПНОГО ----");
            gift2.Update(new Title("Желтые ставни"), "Другой цвет", "https://rutube.ru/video/c6cc4d620b1d4338901770a44b3e82f4/", new Price(2345m));
            Console.WriteLine(gift2);
            Console.WriteLine();

            Console.WriteLine("---- ОТМЕНА БРОНИ ----");
            gift3.Reserve(friendId);
            Console.WriteLine($"До отмены: {gift3}");
            gift3.CancelReservation();
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