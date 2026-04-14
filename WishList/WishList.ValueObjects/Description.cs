using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class Description : ValueObject<string?>
{
    private static readonly DescriptionValidator Validator = new();

    public Description(string? value) : base(Validator, value)
    {
    }

    public static implicit operator string?(Description description) => description?.Value;
}