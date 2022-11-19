using Zio.FileSystems;

namespace Backups.Entities;

public interface IRepository
{
    public string Type { get; }
    string CurrentPath { get; }
    FileSystem FileSystem { get; }
    void CreateDir(string nameDir);
    bool FileExists(string path);
    Stream OpenFile(string path);
    public string GetFileName(string path);
    public string GetDirName(string path);
}