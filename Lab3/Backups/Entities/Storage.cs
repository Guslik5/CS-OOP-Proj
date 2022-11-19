using Backups.Exceptions;

namespace Backups.Entities;

public class Storage
{
    public Storage(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new StorageException("Path storage is null or empty");
        }

        Path = path;
    }

    public string Path { get; }
}