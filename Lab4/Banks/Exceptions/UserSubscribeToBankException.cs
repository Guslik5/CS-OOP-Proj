namespace Banks.Exceptions;

public class UserSubscribeToBankException : Exception
{
    public UserSubscribeToBankException(string messege)
        : base(messege) { }
}