using Banks.Interfaces;

namespace Banks.Commands;

public class Handler
{
    private Handler _prev;
    private Handler _next;
    private ICommand _command;

    public Handler(ICommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        _command = command;
        _prev = null;
        _next = null;
    }

    public void Revert()
    {
        _next?.Revert();
        _command.Revert();
    }

    public bool Execute()
    {
        try
        {
            _command.Execute();
        }
        catch (Exception)
        {
            return false;
        }

        if (_next is null) return true;

        if (_next.Execute())
        {
            return true;
        }

        _command.Revert();

        return false;
    }

    internal void Next(Handler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        if (_next is not null || handler._prev is not null)
        {
            throw new InvalidOperationException("Handler already is set");
        }

        _next = handler;
        handler._prev = this;
    }
}