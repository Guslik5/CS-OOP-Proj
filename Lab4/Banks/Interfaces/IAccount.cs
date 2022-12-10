using Banks.Entities;
using Banks.Moduls;

namespace Banks.Interfaces;

public interface IAccount
{
    string NameAccount { get; }
    IConfig Config { get; set; }
    Bank Bank { get; set; }
    public decimal Money { get; }

    void Replenish(decimal value);
    void TakeOff(decimal value);
}