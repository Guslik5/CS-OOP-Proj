using System.IO.Compression;

namespace Backups.Entities;

public interface IArhiver
{
    void ArhiveFile(ZipArchive zipArchive, string file, IRepository repository);
    void ArhiveDir(ZipArchive zipArchive, string file, IRepository repository, string currentDir);
    ZipArchive CreateZipArhive(string path, IRepository repository);
}