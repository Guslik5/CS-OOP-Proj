using Backups.Entities;
using Backups.Moduls;

namespace Backups.Algorithm;

public class SingleAlgorithm : IAlgorithm
{
    public SingleAlgorithm()
    {
        Arсhiver arсhiver = new Arсhiver();
        Arсhiver = arсhiver;
    }

    public string Type => nameof(SingleAlgorithm);
    private Arсhiver Arсhiver { get; }
    public List<Storage> CreateAlgo(List<BackupObj> listBackupObjs, IRepository repository, string pathDirRestorePoint)
    {
        List<Storage> listStorage = new List<Storage>();
        using var zip = Arсhiver.CreateZipArсhive(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive", repository);
        var storage = new Storage(repository.CurrentPath + "/" + pathDirRestorePoint + "/" + "arhive" + ".zip");
        listStorage.Add(storage);
        foreach (BackupObj backupObj in listBackupObjs)
        {
            if (repository.FileExists(backupObj.Path))
            {
                Arсhiver.ArсhiveFile(zip, backupObj.Path, repository);
            }
            else
            {
                string defaultTest = repository.GetFileName(backupObj.Path);
                Arсhiver.ArсhiveDir(zip, backupObj.Path, repository, defaultTest);
            }
        }

        return listStorage;
    }
}