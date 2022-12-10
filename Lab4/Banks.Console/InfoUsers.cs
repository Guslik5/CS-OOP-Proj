using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class InfoUsers
{
    public static void InfoAboutUsers(CentreBank centreBank)
    {
        var table = new Table();
        table.AddColumn("User");
        table.AddColumn(new TableColumn("Account").Centered());
        var listAllUsersWithCopy = centreBank.Banks.SelectMany(b => b.ListUsers).ToList();
        var listAllUsers = listAllUsersWithCopy.GroupBy(u => u.GuidUser).Select(y => y.First()).ToList();
        foreach (var user in listAllUsers)
        {
            string stringUserAccount = " ";
            foreach (var account in user.ListBanksAccounts)
            {
                stringUserAccount += account.Config.NameConfig + " account in " + account.Bank.Name + System.Environment.NewLine + "Money in account: " + account.Money + System.Environment.NewLine;
            }

            var panel = new Panel(stringUserAccount);
            if (user.Address is null && user.Passport is null)
            {
                table.AddRow(
                    new Markup("Name: " + user.Name + "; Surname:" + user.Surname + System.Environment.NewLine +
                               " Guid:" + user.GuidUser + System.Environment.NewLine +
                               "[red]User refused to enter the passport and address[/]"), panel);
            }
            else
            {
                table.AddRow(
                    new Markup("Name: " + user.Name + "; Surname:" + user.Surname + System.Environment.NewLine +
                               " Guid:" + user.GuidUser + System.Environment.NewLine + "Passport: " + user.Passport + "; Address: " +
                               user.Address), panel);
            }
        }

        AnsiConsole.Write(table);
    }
}