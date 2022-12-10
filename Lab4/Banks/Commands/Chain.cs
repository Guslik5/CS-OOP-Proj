namespace Banks.Commands;

public class Chain
{
    private readonly Handler _root;

    public Chain(params Handler[] handlers)
    {
        _root = handlers.First();
        for (int i = 0; i < handlers.Length - 1; i++)
        {
            handlers[i].Next(handlers[i + 1]);
        }
    }

    public bool Execute() => _root.Execute();

    public void Revert() => _root.Revert();
}