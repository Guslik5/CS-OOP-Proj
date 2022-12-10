using System.Security.Cryptography.X509Certificates;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class UserRemoveAccount
{
    public static void UserRemovingAccount(CentreBank centreBank)
    {
        Guid giudUser = AnsiConsole.Ask<Guid>("Enter Guid user");
        string nameBank = AnsiConsole.Ask<string>("Enter name bank");
        string account = AnsiConsole.Ask<string>("Enter name account for removing");
        var currentUser = centreBank.Banks.SelectMany(b => b.ListUsers)
            .FirstOrDefault(u => u.GuidUser.Equals(giudUser));
        var currentBank = centreBank.Banks.FirstOrDefault(b => b.Name.Equals(nameBank));
        switch (account)
        {
            case "Debit":
                centreBank.UserRemoveAccount(currentUser, currentBank, new DebitAccount());
                break;
            case "Credit":
                centreBank.UserRemoveAccount(currentUser, currentBank, new CreditAccount());
                break;
            case "Deposit":
                centreBank.UserRemoveAccount(currentUser, currentBank, new DepositAccount());
                break;
        }
    }
}