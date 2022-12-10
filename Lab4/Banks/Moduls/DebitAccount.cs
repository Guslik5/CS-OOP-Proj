using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Moduls;

public class DebitAccount : IAccount
{
    private decimal _money = 0;
    public DebitAccount() { }

    public DebitAccount(DebitConfig debitConfig, Bank bank)
    {
        (Config, Bank) = (debitConfig, bank);
    }

    public string NameAccount => "Debit";
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