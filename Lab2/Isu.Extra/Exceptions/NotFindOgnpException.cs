namespace Isu.Extra.Exceptions;

public class NotFindOgnpException : Exception
{
    public NotFindOgnpException(string messege)
        : base(messege) { }
}