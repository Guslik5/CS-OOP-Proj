using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Commands;

public class ReplenishCommand : ICommand
{
    private ConditionCommand _commandCondition;
    private Context _context;

    public ReplenishCommand(Context context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _commandCondition = ConditionCommand.Created;
        _context = context;
    }

    public ConditionCommand ConditionCommand => _commandCondition;
    public void Execute()
    {
        if (_commandCondition != ConditionCommand.Created)
        {
            throw new ConditionException("Invalid condition");
        }

        _context.AccountTo.Replenish(_context.Money);
        _commandCondition = ConditionCommand.Executed;
    }

    public void Revert()
    {
        if (_commandCondition != ConditionCommand.Executed)
        {
            throw new ConditionException("Invalid condition");
        }

        _context.AccountTo.TakeOff(_context.Money);
        _commandCondition = ConditionCommand.Reverted;
    }
}