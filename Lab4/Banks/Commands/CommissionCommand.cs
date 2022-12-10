using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Commands;

public class CommissionCommand : ICommand
{
    private ConditionCommand _commandCondition;
    private Context _context;

    public CommissionCommand(Context context)
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

        _context.AccountFrom.TakeOff(_context.Money * (_context.AccountFrom.Bank.PercentComission / 100));
        _commandCondition = ConditionCommand.Executed;
    }

    public void Revert()
    {
        if (_commandCondition != ConditionCommand.Executed)
        {
            throw new ConditionException("Invalid condition");
        }

        _context.AccountFrom.Replenish(_context.Money * (_context.AccountFrom.Bank.PercentComission / 100));
        _commandCondition = ConditionCommand.Reverted;
    }
}