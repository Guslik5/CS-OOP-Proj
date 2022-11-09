using Zio.FileSystems;

namespace Backups.Entities;

public interface IRepository
{
    void CreateFile();
    void RemoveFile();
}