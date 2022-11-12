namespace Shops.Exceptions;

public class PersonMoneyException : Exception
{
    public PersonMoneyException(string message)
        : base(message) { }
}