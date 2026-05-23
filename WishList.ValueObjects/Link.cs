using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class Link : ValueObject<string?>
{
    private static readonly LinkValidator Validator = new();

    public Link(string? value) : base(Validator, value)
    {
    }

    public static implicit operator string?(Link link) => link?.Value;
}