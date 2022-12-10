using Banks.Entities;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class Transaction
{
    public static void TransactionOnn(CentreBank centreBank)
    {
        Guid giudUserFrom = AnsiConsole.Ask<Guid>("Enter Guid user From");
        Guid giudUserTo = AnsiConsole.Ask<Guid>("Enter Guid user To");
        string bankFrom = AnsiConsole.Ask<string>("Enter name bank From");
        string bankTo = AnsiConsole.Ask<string>("Enter name bank To");
        string accountFrom = AnsiConsole.Ask<string>("Enter name account From");
        string accountTo = AnsiConsole.Ask<string>("Enter name account To");
        decimal money = AnsiConsole.Ask<decimal>("Enter count of money for transaction");
        User currentUserFrom = centreBank.Banks.SelectMany(b => b.ListUsers)
            .FirstOrDefault(u => u.GuidUser.Equals(giudUserFrom));
        User currentUserTo = centreBank.Banks.SelectMany(b => b.ListUsers)
            .FirstOrDefault(u => u.GuidUser.Equals(giudUserTo));
        centreBank.Transaction(currentUserFrom, bankFrom, accountFrom, currentUserTo, bankTo, accountTo, money);
    }
}