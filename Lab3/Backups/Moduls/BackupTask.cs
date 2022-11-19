using Backups.Algorithm;
using Backups.Entities;

namespace Backups.Moduls;

public class BackupTask
{
    private Backup _backup = new Backup();

    public BackupTask(IAlgorithm algorithm, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(algorithm, "Algorithm is null");
        ArgumentNullException.ThrowIfNull(algorithm, "Name is null");
        ArgumentNullException.ThrowIfNull(algorithm, "Repository is null");
        (Algorithm, Repository) = (algorithm, repository);
    }

    internal IAlgorithm Algorithm { get; }
    internal IRepository Repository { get; }
    internal Backup Backup => _backup;

    public void AddFile(string path)
    {
        _backup.Config.AddFile(new BackupObj(path));
    }

    public void RemoveFile(string path)
    {
        _backup.Config.RemoveFile(new BackupObj(path));
    }

    public void ChangeAlgorithm(IAlgorithm algorithm)
    {
        _backup.Config.ChangeAlgo(algorithm);
    }

    public List<RestorePoint> ViewRestorePoint()
    {
        return new List<RestorePoint>(_backup.ListRestorePoint);
    }

    public void Execute()
    {
        List<Storage> listStorage = new List<Storage>();
        string pathName = "RestorePoint" + _backup.GetCountRestorePoint().ToString();
        Repository.CreateDir(pathName);
        listStorage = _backup.Config.Algorithm.CreateAlgo(_backup.Config.ListBackupObjs, Repository, pathName);
        _backup.AddRestorePoint(listStorage, DateTime.Now);
    }
}