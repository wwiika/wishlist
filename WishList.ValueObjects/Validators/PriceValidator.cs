using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class PriceValidator : IValidator<decimal>
{
    public static decimal MinPrice => 0;
    public static decimal MaxPrice => 1_000_000;

    public void Validate(decimal value)
    {
        if (value < MinPrice)
            throw new InvalidPriceException("Цена не может быть отрицательной");

        if (value > MaxPrice)
            throw new InvalidPriceException($"Цена не может превышать {MaxPrice} рублей");
    }
}