namespace Backups.Exceptions;

public class StorageException : Exception
{
    public StorageException(string messege)
        : base(messege) { }
}