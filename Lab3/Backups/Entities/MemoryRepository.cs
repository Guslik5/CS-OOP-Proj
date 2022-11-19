using Backups.Exceptions;
using Zio.FileSystems;

namespace Backups.Entities;

public class MemoryRepository : IRepository
{
    private FileSystem _fs;
    private string _currentDir;
    public MemoryRepository(string path, string nameRepository, FileSystem fs)
    {
        _currentDir = path + "/" + nameRepository;
        fs.CreateDirectory(_currentDir);
        _fs = fs;
        CurrentPath = _currentDir;
        FileSystem = _fs;
    }

    public string CurrentPath { get; }
    public FileSystem FileSystem { get; }

    public void CreateDir(string nameDir)
    {
        var newDir = _currentDir + "/" + nameDir;
        _fs.CreateDirectory(newDir);
    }

    public void CreateDir(string nameDir, string path)
    {
        var newDir = path + "/" + nameDir;
        _fs.CreateDirectory(newDir);
    }

    public Stream OpenFile(string path)
    {
        if (!FileExists(path))
        {
            throw new OpenFileException("File not found or file is dir");
        }

        return _fs.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    public string GetDirName(string path)
    {
        ArgumentNullException.ThrowIfNull(path, "path = null");
        return Path.GetDirectoryName(path);
    }

    public bool FileExists(string path)
    {
        return _fs.FileExists(path);
    }
}