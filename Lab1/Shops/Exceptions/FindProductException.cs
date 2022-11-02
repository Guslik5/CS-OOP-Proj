namespace Shops.Exceptions;

public class FindProductException : Exception
{
    public FindProductException(string message)
        : base(message) { }
}