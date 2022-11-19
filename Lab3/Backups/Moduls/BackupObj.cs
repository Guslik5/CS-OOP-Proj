using Backups.Exceptions;

namespace Backups.Moduls;

public class BackupObj : IEquatable<BackupObj>
{
    public BackupObj(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new BackupObjException("BackupObj path is null or empty");
        }

        Path = path;
    }

    public string Path { get; }

    public bool Equals(BackupObj other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path == other.Path;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BackupObj)obj);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }
}