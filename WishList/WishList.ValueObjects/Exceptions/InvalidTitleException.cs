namespace WishList.ValueObjects.Exceptions;

public class InvalidTitleException : ArgumentException
{
    public InvalidTitleException(string message) : base(message)
    {
    }
}