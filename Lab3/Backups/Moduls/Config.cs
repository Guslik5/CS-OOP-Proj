using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Moduls;

public class Config
{
    private readonly List<BackupObj> _listBackupObjs = new List<BackupObj>();
    private IAlgorithm _algorithm = new SingleAlgorithm();
    public Config() { }

    public List<BackupObj> ListBackupObjs => _listBackupObjs;
    internal IAlgorithm Algorithm => _algorithm;

    public void AddFile(string path)
    {
        ArgumentNullException.ThrowIfNull(path, "Path is null");
        var obj = _listBackupObjs.Where(o => o.Path.Equals(path)).FirstOrDefault();
        if (obj is not null)
        {
            throw new FileAddedException("File has already been added");
        }

        _listBackupObjs.Add(new BackupObj(path));
    }

    public void RemoveFile(string path)
    {
        ArgumentNullException.ThrowIfNull(path, "Path is null");
        var removeObj = _listBackupObjs.Where(obj => obj.Path.Equals(path)).FirstOrDefault();
        if (removeObj is null)
        {
            throw new NotFoundFileException("File is not found");
        }

        _listBackupObjs.Remove(removeObj);
    }

    public void ChangeAlgo(string algo)
    {
        switch (algo)
        {
            case "single":
                _algorithm = new SingleAlgorithm();
                break;
            case "split":
                _algorithm = new SplitAlgorithm();
                break;
            default:
                throw new InvalidAlgorithmException("Invalid name of Algorithm");
        }
    }
}