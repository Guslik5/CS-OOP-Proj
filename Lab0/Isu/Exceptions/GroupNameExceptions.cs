namespace Isu.Exceptions;

public class GroupNameExceptions : Exception
{
    public GroupNameExceptions(string message, string val)
        : base(message)
    {
        Value = val;
    }

    public string Value { get; }
}