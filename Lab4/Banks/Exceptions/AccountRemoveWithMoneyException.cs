namespace Banks.Exceptions;

public class AccountRemoveWithMoneyException : Exception
{
    public AccountRemoveWithMoneyException(string messege)
        : base(messege) { }
}