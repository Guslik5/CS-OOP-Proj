using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Moduls;

public class DepositAccount : IAccount
{
    private decimal _money = 0;
    public DepositAccount() { }
    public DepositAccount(DepositConfig depositConfig, Bank bank)
    {
        (Config, Bank) = (depositConfig, bank);
    }

    public string NameAccount => "Deposit";
    public IConfig Config { get; set; }
    public Bank Bank { get; set; }

    public decimal Money => _money;

    public void Replenish(decimal value)
    {
        _money += value;
    }

    public void TakeOff(decimal value)
    {
        if (value > _money)
        {
            throw new MoneyException("Insufficient funds");
        }

        _money -= value;
    }
}