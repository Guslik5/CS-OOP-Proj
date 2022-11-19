using Backups.Entities;

namespace Backups.Moduls;

public class RestorePoint
{
    private List<Storage> _listStorage = new List<Storage>();
    private DateTime _dateTime;

    public RestorePoint(List<Storage> storages, DateTime? dateTime)
    {
        ArgumentNullException.ThrowIfNull(storages, "storages is null");
        _listStorage = storages;

        _dateTime = dateTime ?? DateTime.Now;
    }
}