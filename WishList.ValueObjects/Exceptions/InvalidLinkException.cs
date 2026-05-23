namespace WishList.ValueObjects.Exceptions;

public class InvalidLinkException : ArgumentException
{
    public InvalidLinkException(string message) : base(message)
    {
    }
}