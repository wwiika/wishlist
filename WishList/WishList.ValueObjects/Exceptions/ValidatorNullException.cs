namespace WishList.ValueObjects.Exceptions;

public class ValidatorNullException : ArgumentNullException
{
    public ValidatorNullException(string paramName)
        : base(paramName, "Валидатор не может быть пустым")
    {
    }
}