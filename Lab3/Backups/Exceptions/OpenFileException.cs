namespace Backups.Exceptions;

public class OpenFileException : Exception
{
    public OpenFileException(string messege)
        : base(messege) { }
}