namespace Banks.Exceptions;

public class UserException : Exception
{
    public UserException(string messege)
        : base(messege) { }
}