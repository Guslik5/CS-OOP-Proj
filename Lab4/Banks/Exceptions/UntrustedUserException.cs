namespace Banks.Exceptions;

public class UntrustedUserException : Exception
{
    public UntrustedUserException(string messege)
        : base(messege) { }
}