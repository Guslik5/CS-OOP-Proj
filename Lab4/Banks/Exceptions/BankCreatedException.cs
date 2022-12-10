namespace Banks.Exceptions;

public class BankCreatedException : Exception
{
    public BankCreatedException(string messege)
        : base(messege) { }
}