namespace Backups.Exceptions;

public class FileAddedException : Exception
{
    public FileAddedException(string messege)
        : base(messege) { }
}