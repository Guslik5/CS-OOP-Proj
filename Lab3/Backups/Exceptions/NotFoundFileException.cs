namespace Backups.Exceptions;

public class NotFoundFileException : Exception
{
    public NotFoundFileException(string messege)
        : base(messege) { }
}