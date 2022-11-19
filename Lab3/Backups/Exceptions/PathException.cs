namespace Backups.Exceptions;

public class PathException : Exception
{
    public PathException(string messege)
        : base(messege) { }
}