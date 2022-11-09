namespace Isu.Extra.Exceptions;

public class NotHaveSubjectException : Exception
{
    public NotHaveSubjectException(string messege)
        : base(messege) { }
}