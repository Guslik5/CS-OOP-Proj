namespace Shop.Exceptions;

public class ProductBuyException : Exception
{
    public ProductBuyException(string message)
        : base(message) { }
}