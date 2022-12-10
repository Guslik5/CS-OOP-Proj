namespace Banks.Exceptions;

public class MoneyException : Exception
{
    public MoneyException(string messege)
        : base(messege) { }
}