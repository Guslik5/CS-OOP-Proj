using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Moduls;

public class CreditAccount : IAccount
{
    private decimal _money = 0;
    public CreditAccount() { }
    public CreditAccount(CreditConfig creditConfig, Bank bank)
    {
        (Config, Bank) = (creditConfig, bank);
    }

    public string NameAccount => "Credit";
    public IConfig Config { get; set; }
    public Bank Bank { get; set; }
    public decimal Money => _money;

    public void Replenish(decimal value)
    {
        _money += value;
    }

    public void TakeOff(decimal value)
    {
        _money -= value;
    }
}