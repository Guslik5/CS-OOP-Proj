using Backups.Algorithm;
using Backups.Exceptions;

namespace Backups.Moduls;

public class Config
{
    private readonly List<BackupObj> _listBackupObjs = new List<BackupObj>();
    private IAlgorithm _algorithm = new SingleAlgorithm();
    public Config() { }

    public List<BackupObj> ListBackupObjs => _listBackupObjs;
    internal IAlgorithm Algorithm => _algorithm;

    public void AddFile(BackupObj backupObj)
    {
        ArgumentNullException.ThrowIfNull(backupObj, "Path is null");
        var obj = _listBackupObjs.Where(o => o.Equals(backupObj)).FirstOrDefault();
        if (obj is not null)
        {
            throw new FileAddedException("File has already been added");
        }

        _listBackupObjs.Add(backupObj);
    }

    public void RemoveFile(BackupObj backupObj)
    {
        ArgumentNullException.ThrowIfNull(backupObj, "Path is null");
        var removeObj = _listBackupObjs.Where(obj => obj.Path.Equals(backupObj.Path)).FirstOrDefault();
        if (removeObj is null)
        {
            throw new NotFoundFileException("File is not found");
        }

        _listBackupObjs.Remove(removeObj);
    }

    public void ChangeAlgo(IAlgorithm algo)
    {
        _algorithm = algo;
    }
}