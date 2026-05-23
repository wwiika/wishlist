using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class UserIdValidator : IValidator<Guid>
{
    public void Validate(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidUserIdException("UserId не может быть пустым");
    }
}