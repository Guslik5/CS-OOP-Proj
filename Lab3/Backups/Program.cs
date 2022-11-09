using Zio;
using Zio.FileSystems;
namespace Backups;

public class Program
{
    public static void Main()
    {
        /*var fs = new PhysicalFileSystem();
        fs.CreateDirectory("/mnt/c/Users/Guslik/Desktop/test/testing");*/
        var fsZip = new ZipArchiveFileSystem("C://Users/Guslik/Desktop/test/ZipArhive.zip");
        fsZip.CreateFile("C://Users/Guslik/Desktop/test/ZipArhive.zip/text");
        fsZip.WriteAllText("text", "Privet");
    }
}