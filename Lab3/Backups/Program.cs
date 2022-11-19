using Backups.Algorithm;
using Backups.Entities;
using Backups.Moduls;
using Zio.FileSystems;
namespace Backups;

public class Program
{
    public static void Main()
    {
        var backupTask = new BackupTask(new SingleAlgorithm(), new PhisicalRepository("/mnt/c/Users/Guslik/Desktop/test", "phisicalTask", new PhysicalFileSystem()));
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/hello.txt");
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/hello2.txt");
        backupTask.Execute();
        backupTask.AddFile("/mnt/c/Users/Guslik/Desktop/FileForBackup/good");
        backupTask.ChangeAlgorithm(new SplitAlgorithm());
        backupTask.Execute();
        var backupTask2 = new BackupTask(new SingleAlgorithm(), new PhisicalRepository("/mnt/c/Users/Guslik/Desktop/test", "phisicalTask2", new PhysicalFileSystem()));
        backupTask2.AddFile("/mnt/c/Users/Guslik/Desktop/university/2 курс");
        backupTask2.Execute();
    }
}