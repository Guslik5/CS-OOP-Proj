namespace Shops.Exceptions;

public class BuyProductException : Exception
{
    public BuyProductException(string message)
        : base(message) { }
}