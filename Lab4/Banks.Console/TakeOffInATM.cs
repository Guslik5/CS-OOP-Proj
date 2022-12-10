using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class TakeOffInATM
{
    public static void TakeOff(CentreBank centreBank)
    {
        Guid giudUser = AnsiConsole.Ask<Guid>("Enter Guid user");
        string nameBank = AnsiConsole.Ask<string>("Enter name bank");
        string account = AnsiConsole.Ask<string>("Enter name account for replenish");
        decimal money = AnsiConsole.Ask<decimal>("Enter count of money for replenish");
        var currentUser = centreBank.Banks.SelectMany(b => b.ListUsers)
            .FirstOrDefault(u => u.GuidUser.Equals(giudUser));
        var currentBank = centreBank.Banks.FirstOrDefault(b => b.Name.Equals(nameBank));
        centreBank.TakeOffInATM(currentUser, currentBank, account, money);
    }
}