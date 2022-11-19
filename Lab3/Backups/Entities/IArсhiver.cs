using System.IO.Compression;

namespace Backups.Entities;

public interface IArсhiver
{
    void ArсhiveFile(ZipArchive zipArchive, string file, IRepository repository);
    void ArсhiveDir(ZipArchive zipArchive, string file, IRepository repository, string currentDir);
    ZipArchive CreateZipArсhive(string path, IRepository repository);
}