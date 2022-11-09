namespace Backups.Moduls;

public class BackupObj
{
    public BackupObj(string path)
    {
        Path = path;
    }

    public string Path { get; }
}