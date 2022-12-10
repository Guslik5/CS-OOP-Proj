namespace Banks.Exceptions;

public class BankNameForTransactionException : Exception
{
    public BankNameForTransactionException(string messege)
        : base(messege) { }
}