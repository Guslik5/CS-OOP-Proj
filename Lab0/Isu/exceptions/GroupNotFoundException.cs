namespace Isu.exceptions;

public class GroupNotFoundException : Exception
{
    public GroupNotFoundException(string message, string val)
        : base(message)
    {
        Value = val;
    }

    public string Value { get; }
}