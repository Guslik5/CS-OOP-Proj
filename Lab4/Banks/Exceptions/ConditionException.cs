using Banks.Commands;

namespace Banks.Exceptions;

public class ConditionException : Exception
{
    public ConditionException(string messege)
        : base(messege) { }
}