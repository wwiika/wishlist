using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class UsernameValidator : IValidator<string>
{
    public static int MinLength => 3;
    public static int MaxLength => 30;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidUsernameException("Имя пользователя не может быть пустым");

        value = value.Trim();

        if (value.Length < MinLength)
            throw new InvalidUsernameException($"Имя пользователя должно содержать минимум {MinLength} символа");

        if (value.Length > MaxLength)
            throw new InvalidUsernameException($"Имя пользователя не может быть длиннее {MaxLength} символов");
    }
}