using Backups.Entities;
using Backups.Moduls;

namespace Backups.Algorithm;

public interface IAlgorithm
{
    public List<Storage> CreateAlgo(List<BackupObj> listBackupObjs, IRepository repository, string pathDirRestorePoint);
}