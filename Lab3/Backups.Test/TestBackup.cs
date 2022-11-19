using Backups.Algorithm;
using Backups.Entities;
using Backups.Moduls;
using Xunit;
using Zio;
using Zio.FileSystems;

namespace Backups.Test;

public class TestBackup
{
    [Fact(Skip = "phisicalFileSystem")]
    public void TestingPhisicalArhive()
    {
        var fs = new PhysicalFileSystem();
        var backupTask = new BackupTask(new SingleAlgorithm(), new PhisicalRepository("/mnt/c/Users/Guslik/Desktop/test", "phisicalTask", fs));
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/hello.txt");
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/hello2.txt");
        backupTask.Execute();
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/good");
        backupTask.ChangeAlgorithm(new SplitAlgorithm());
        backupTask.Execute();
        List<RestorePoint> list = backupTask.ViewRestorePoint();
        list.Clear();
        Assert.Equal(2, backupTask.ViewRestorePoint().Count);
    }

    [Fact]
    public void TestingMemoryArhive()
    {
        var fs = new MemoryFileSystem();
        fs.CreateDirectory("/home");
        fs.CreateDirectory("/home/Test");
        fs.CreateDirectory("/home/backup");
        var stream = fs.CreateFile("/home/Test/hello.txt");
        stream.Dispose();
        stream = fs.CreateFile("/home/Test/hello2.txt");
        stream.Dispose();

        var backupTask = new BackupTask(new SingleAlgorithm(), new MemoryRepository("/home/backup", "testing", fs));
        backupTask.AddFile("/home/Test/hello.txt");
        backupTask.AddFile("/home/Test/hello2.txt");
        backupTask.Execute();
        Assert.NotEmpty(backupTask.ViewRestorePoint());
    }
}