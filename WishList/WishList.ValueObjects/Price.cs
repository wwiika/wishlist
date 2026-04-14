using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class Price : ValueObject<decimal>
{
    private static readonly PriceValidator Validator = new();

    public Price(decimal value) : base(Validator, value)
    {
    }

    public static implicit operator decimal(Price price) => price.Value;
}