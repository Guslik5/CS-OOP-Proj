namespace Banks.Exceptions;

public class AccountException : Exception
{
    public AccountException(string messege)
        : base(messege) { }
}