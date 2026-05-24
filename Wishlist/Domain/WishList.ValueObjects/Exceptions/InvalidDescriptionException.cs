namespace WishList.ValueObjects.Exceptions;

public class InvalidDescriptionException : ArgumentException
{
    public InvalidDescriptionException(string message) : base(message)
    {
    }
}