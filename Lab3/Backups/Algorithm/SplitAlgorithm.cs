using Backups.Entities;
using Backups.Moduls;

namespace Backups.Algorithm;

public class SplitAlgorithm : IAlgorithm
{
    public SplitAlgorithm()
    {
        Arсhiver arсhiver = new Arсhiver();
        Arсhiver = arсhiver;
    }

    public string Type => nameof(SplitAlgorithm);
    private Arсhiver Arсhiver { get; }
    public List<Storage> CreateAlgo(List<BackupObj> listBackupObjs, IRepository repository, string pathDirRestorePoint)
    {
        List<Storage> listStorage = new List<Storage>();
        var countOfZipArhive = 1;
        foreach (BackupObj backupObj in listBackupObjs)
        {
            using var zip = Arсhiver.CreateZipArсhive(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + countOfZipArhive.ToString(), repository);
            listStorage.Add(new Storage(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + countOfZipArhive.ToString() + ".zip"));
            if (repository.FileExists(backupObj.Path))
            {
                Arсhiver.ArсhiveFile(zip, backupObj.Path, repository);
            }
            else
            {
                string defaultTest = repository.GetFileName(backupObj.Path);
                Arсhiver.ArсhiveDir(zip, backupObj.Path, repository, defaultTest);
            }

            countOfZipArhive++;
        }

        return listStorage;
    }
}