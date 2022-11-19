namespace Backups.Exceptions;

public class BackupObjException : Exception
{
    public BackupObjException(string messege)
        : base(messege) { }
}