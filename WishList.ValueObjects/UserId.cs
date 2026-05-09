using WishList.ValueObjects.Base;

namespace WishList.ValueObjects;

public class UserId : ValueObject<Guid>
{
    private static readonly UserIdValidator Validator = new();

    public UserId(Guid value) : base(Validator, value)
    {
    }

    public static UserId New() => new(Guid.NewGuid());

    private class UserIdValidator : IValidator<Guid>
    {
        public void Validate(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("UserId не может быть пустым", nameof(value));
        }
    }
}