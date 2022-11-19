using System.IO.Compression;
using Zio;

namespace Backups.Entities;

public class Arhiver : IArhiver
{
    public void ArhiveFile(ZipArchive zipArchive, string file, IRepository repository)
    {
        using var entryStream = zipArchive.CreateEntry(repository.GetFileName(file)).Open();
        using var fileStream = repository.FileSystem.OpenFile(file, FileMode.Open, FileAccess.Read);
        fileStream.CopyTo(entryStream);
    }

    public void ArhiveDir(ZipArchive zipArchive, string file, IRepository repository, string currentDir)
    {
        var listFilesInDir = repository.FileSystem.EnumerateFileSystemEntries(file);
        var listFiles = listFilesInDir.Where(p => repository.FileExists(p.FullName));
        var listDir = listFilesInDir.Where(p => !repository.FileExists(p.FullName));
        foreach (var path in listFiles)
        {
            using var entryStream = zipArchive.CreateEntry(currentDir + "/" + repository.GetFileName(path.FullName)).Open();
            using var fileStream = repository.FileSystem.OpenFile(path.FullName, FileMode.Open, FileAccess.Read);
            fileStream.CopyTo(entryStream);
        }

        foreach (var dir in listDir)
        {
            currentDir += "/" + repository.GetFileName(dir.FullName);
            ArhiveDir(zipArchive, dir.FullName, repository, currentDir);
            currentDir = repository.GetDirName(currentDir);
        }
    }

    public ZipArchive CreateZipArhive(string path, IRepository repository)
    {
        var fs = repository.FileSystem;
        var stream = fs.OpenFile(path + ".zip", FileMode.OpenOrCreate, FileAccess.Write);
        ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create);
        return zip;
    }
}