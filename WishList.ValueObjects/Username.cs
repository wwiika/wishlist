using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class Username : ValueObject<string>
{
    private static readonly UsernameValidator Validator = new();

    public Username(string value) : base(Validator, value)
    {
    }

    public static implicit operator string(Username username) => username.Value;
}