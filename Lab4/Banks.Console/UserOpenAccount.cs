using Banks.Entities;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class UserOpenAccount
{
    public static void UserOpeningAccount(CentreBank centreBank)
    {
        if (AnsiConsole.Confirm("Do you want to open an account with an existing user"))
        {
            Guid guidUser = AnsiConsole.Ask<Guid>("Enter Guid user");
            string bankName = AnsiConsole.Ask<string>("Enter name bank");
            var bankCurrent = centreBank.Banks.Where(b => b.Name.Equals(bankName)).FirstOrDefault();
            string accountCurrent = AnsiConsole.Ask<string>("What's account do you want open?");
            var userCurrent = centreBank.Banks.SelectMany(b => b.ListUsers).Where(u => u.GuidUser.Equals(guidUser)).First();
            switch (accountCurrent)
            {
                case "Debit":
                    centreBank.UserOpenAccount(userCurrent, bankCurrent, new DebitAccount());
                    break;
                case "Credit":
                    centreBank.UserOpenAccount(userCurrent, bankCurrent, new CreditAccount());
                    break;
                case "Deposit":
                    centreBank.UserOpenAccount(userCurrent, bankCurrent, new DepositAccount());
                    break;
            }

            return;
        }

        User currentUser;
        string nameUser = AnsiConsole.Ask<string>("Enter name user");
        string surnameUser = AnsiConsole.Ask<string>("Enter surname user");
        if (AnsiConsole.Confirm("Do you want Enter you Passport and address"))
        {
            string passport = AnsiConsole.Ask<string>("Enter passport user");
            string address = AnsiConsole.Ask<string>("Enter address user");
            currentUser = new UserBuilder().WithName(nameUser).WithSurname(surnameUser).WithPassport(passport).WithAdress(address).Build();
        }
        else
        {
            currentUser = new UserBuilder().WithName(nameUser).WithSurname(surnameUser).Build();
        }

        string nameBank = AnsiConsole.Ask<string>("Enter name bank");
        string account = AnsiConsole.Ask<string>("What's account do you want open?");
        var currentBank = centreBank.Banks.Where(b => b.Name.Equals(nameBank)).FirstOrDefault();
        switch (account)
        {
            case "Debit":
                centreBank.UserOpenAccount(currentUser, currentBank, new DebitAccount());
                break;
            case "Credit":
                centreBank.UserOpenAccount(currentUser, currentBank, new CreditAccount());
                break;
            case "Deposit":
                centreBank.UserOpenAccount(currentUser, currentBank, new DepositAccount());
                break;
        }
    }
}