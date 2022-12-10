namespace Banks.Exceptions;

public class AccountForTransactionException : Exception
{
    public AccountForTransactionException(string messege)
        : base(messege) { }
}