namespace Shop.Exceptions;

public class ShopException : Exception
{
    public ShopException(string message)
        : base(message) { }
}