namespace Shop.Exceptions;

public class CountElementsException : Exception
{
    public CountElementsException(string message)
        : base(message) { }
}