using Banks.Entities;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class ReplenishInATM
{
    public static void Replenish(CentreBank centreBank)
    {
        Guid giudUser = AnsiConsole.Ask<Guid>("Enter Guid user");
        string nameBank = AnsiConsole.Ask<string>("Enter name bank");
        string account = AnsiConsole.Ask<string>("Enter name account for replenish");
        decimal money = AnsiConsole.Ask<decimal>("Enter count of money for replenish");
        User currentUser = centreBank.Banks.SelectMany(b => b.ListUsers)
            .FirstOrDefault(u => u.GuidUser.Equals(giudUser));
        Bank currentBank = centreBank.Banks.FirstOrDefault(b => b.Name.Equals(nameBank));
        centreBank.ReplenishInATM(currentUser, currentBank, account, money);
    }
}