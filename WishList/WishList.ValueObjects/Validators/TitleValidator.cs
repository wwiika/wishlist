using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class TitleValidator : IValidator<string>
{
    public static int MinLength => 3;
    public static int MaxLength => 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidTitleException("Название подарка не может быть пустым");

        value = value.Trim();

        if (value.Length < MinLength)
            throw new InvalidTitleException($"Название должно содержать минимум {MinLength} символа");

        if (value.Length > MaxLength)
            throw new InvalidTitleException($"Название не может быть длиннее {MaxLength} символов");
    }
}