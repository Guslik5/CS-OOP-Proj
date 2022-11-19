using Backups.Entities;

namespace Backups.Moduls;

public class Backup
{
    private Config _currentConfig = new Config();
    private List<RestorePoint> _listRestorePoint = new List<RestorePoint>();
    public Backup() { }
    internal List<RestorePoint> ListRestorePoint => _listRestorePoint;
    internal Config Config => _currentConfig;

    public void AddRestorePoint(List<Storage> listStorage)
    {
        ArgumentNullException.ThrowIfNull(listStorage, "listStorage is null");
        _listRestorePoint.Add(new RestorePoint(listStorage));
    }

    public int GetCountRestorePoint()
    {
        return _listRestorePoint.Count();
    }
}