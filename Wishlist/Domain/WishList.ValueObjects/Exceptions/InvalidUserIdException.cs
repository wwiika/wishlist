namespace WishList.ValueObjects.Exceptions;

public class InvalidUserIdException : ArgumentException
{
    public InvalidUserIdException(string message) : base(message)
    {
    }
}