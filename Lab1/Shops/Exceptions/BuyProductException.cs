namespace Shop.Exceptions;

public class BuyProductException : Exception
{
    public BuyProductException(string message)
        : base(message) { }
}