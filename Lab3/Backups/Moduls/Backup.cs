namespace Backups.Moduls;

public class Backup
{
    private Config _currentConfig = new Config();
    private List<RestorePoint> _listRestorePoint = new List<RestorePoint>();
    public Backup() { }
}