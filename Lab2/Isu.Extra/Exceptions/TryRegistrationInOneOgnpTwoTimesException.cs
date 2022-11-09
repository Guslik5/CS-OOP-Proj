namespace Isu.Extra.Exceptions;

public class TryRegistrationInOneOgnpTwoTimesException : Exception
{
    public TryRegistrationInOneOgnpTwoTimesException(string messege)
        : base(messege) { }
}