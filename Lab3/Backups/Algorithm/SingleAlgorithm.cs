using Backups.Entities;
using Backups.Moduls;

namespace Backups.Algorithm;

public class SingleAlgorithm : IAlgorithm
{
    public SingleAlgorithm()
    {
        Arhiver arhiver = new Arhiver();
        Arhiver = arhiver;
    }

    private Arhiver Arhiver { get; }
    public List<Storage> CreateAlgo(List<BackupObj> listBackupObjs, IRepository repository, string pathDirRestorePoint)
    {
        List<Storage> listStorage = new List<Storage>();
        using var zip = Arhiver.CreateZipArhive(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive", repository);
        var storage = new Storage(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + ".zip");
        listStorage.Add(storage);
        foreach (BackupObj backupObj in listBackupObjs)
        {
            if (repository.FileExists(backupObj.Path))
            {
                Arhiver.ArhiveFile(zip, backupObj.Path, repository);
            }
            else
            {
                string defaultTest = repository.GetFileName(backupObj.Path);
                Arhiver.ArhiveDir(zip, backupObj.Path, repository, defaultTest);
            }
        }

        return listStorage;
    }
}