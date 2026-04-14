using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class DescriptionValidator : IValidator<string?>
{
    public static int MaxLength => 500;

    public void Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (value.Length > MaxLength)
            throw new InvalidTitleException($"Описание не может быть длиннее {MaxLength} символов");
    }
}