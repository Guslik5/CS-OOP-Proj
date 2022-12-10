using Banks.Interfaces;

namespace Banks.Commands;

public class Context
{
    public Context(IAccount accountFrom, IAccount accountTo, decimal money)
    {
        ArgumentNullException.ThrowIfNull(accountFrom);
        ArgumentNullException.ThrowIfNull(accountTo);
        ArgumentNullException.ThrowIfNull(money);
        (AccountFrom, AccountTo, Money) = (accountFrom, accountTo, money);
    }

    public IAccount AccountFrom { get; }
    public IAccount AccountTo { get; }
    public decimal Money { get; }
}