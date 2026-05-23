using WishList.ValueObjects.Base;
using WishList.ValueObjects.Exceptions;

namespace WishList.ValueObjects.Validators;

public class LinkValidator : IValidator<string?>
{
    public static int MaxLength => 255;

    public void Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;

        if (value.Length > MaxLength)
            throw new InvalidLinkException($"Ссылка не может быть длиннее {MaxLength} символов");

        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new InvalidLinkException("Ссылка должна быть корректным URL");
    }
}