namespace Shops.Exceptions;

public class BuyProductsException : Exception
{
    public BuyProductsException(string message)
        : base(message) { }
}