namespace Backups.Exceptions;

public class InvalidAlgorithmException : Exception
{
    public InvalidAlgorithmException(string messege)
        : base(messege) { }
}