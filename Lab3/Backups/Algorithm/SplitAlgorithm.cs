using Backups.Entities;
using Backups.Moduls;

namespace Backups.Algorithm;

public class SplitAlgorithm : IAlgorithm
{
    public SplitAlgorithm()
    {
        Arhiver arhiver = new Arhiver();
        Arhiver = arhiver;
    }

    private Arhiver Arhiver { get; }
    public List<Storage> CreateAlgo(List<BackupObj> listBackupObjs, IRepository repository, string pathDirRestorePoint)
    {
        List<Storage> listStorage = new List<Storage>();
        var countOfZipArhive = 1;
        foreach (BackupObj backupObj in listBackupObjs)
        {
            using var zip = Arhiver.CreateZipArhive(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + countOfZipArhive.ToString(), repository);
            listStorage.Add(new Storage(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + countOfZipArhive.ToString() + ".zip"));
            if (repository.FileExists(backupObj.Path))
            {
                Arhiver.ArhiveFile(zip, backupObj.Path, repository);
            }
            else
            {
                string defaultTest = repository.GetFileName(backupObj.Path);
                Arhiver.ArhiveDir(zip, backupObj.Path, repository, defaultTest);
            }

            countOfZipArhive++;
        }

        return listStorage;
    }
}