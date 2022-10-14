namespace Shop.Exceptions;

public class ProductChangeException : Exception
{
    public ProductChangeException(string message)
        : base(message) { }
}