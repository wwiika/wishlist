namespace WishList.ValueObjects.Exceptions;

public class InvalidPriceException : ArgumentOutOfRangeException
{
    public InvalidPriceException(string message) : base(message)
    {
    }
}