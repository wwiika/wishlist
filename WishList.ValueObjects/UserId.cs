using WishList.ValueObjects.Base;
using WishList.ValueObjects.Validators;

namespace WishList.ValueObjects;

public class UserId : ValueObject<Guid>
{
    private static readonly UserIdValidator Validator = new();

    public UserId(Guid value) : base(Validator, value)
    {
    }

    public static UserId New() => new(Guid.NewGuid());


}