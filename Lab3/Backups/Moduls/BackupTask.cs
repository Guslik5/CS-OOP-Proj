namespace Backups.Moduls;

public class BackupTask
{
    private Config _config = new Config();
    private Backup _backup = new Backup();

    public BackupTask() { }

    internal Config Config => _config;

    public void AddFile(string path)
    {
        _config.AddFile(path);
    }

    public void RemoveFile(string path)
    {
        _config.RemoveFile(path);
    }

    public void ChangeAlgorithm(string algorithm)
    {
        _config.ChangeAlgo(algorithm);
    }

    public void Execute()
    {
        _config.Algorithm.CreateAlgo();
    }
}