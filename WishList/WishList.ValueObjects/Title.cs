using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class Title : ValueObject<string>
{
    private static readonly TitleValidator Validator = new();

    public Title(string value) : base(Validator, value)
    {
    }

    public static implicit operator string(Title title) => title.Value;
}