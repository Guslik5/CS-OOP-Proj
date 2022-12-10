using Banks.Entities;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class ChangeConfig
{
    public static void ChangingConfig(CentreBank centreBank)
    {
        string nameBank = AnsiConsole.Ask<string>("Enter the name of the bank");
        string configName = AnsiConsole.Ask<string>("Enter the name config");
        Bank currentBank = centreBank.Banks.FirstOrDefault(b => b.Name.Equals(nameBank));
        switch (configName)
        {
            case "Debit":
                decimal percentDebit = AnsiConsole.Ask<decimal>("Enter the percentage on the [blue]debit[/] account balance");
                decimal firstDebit = 0;
                decimal lastDebit = decimal.MaxValue;
                var configDebit = new DebitConfig(new List<HelperforConfig>() { new HelperforConfig(firstDebit, lastDebit, percentDebit) });
                centreBank.ChangeConfig(currentBank, configDebit);
                break;
            case "Credit":
                decimal percentCredit = AnsiConsole.Ask<decimal>("Enter the percentage on the [blue]Credit[/] account balance");
                decimal firstCredit = decimal.MinValue;
                decimal lastCredit = decimal.MaxValue;
                var configCredit = new CreditConfig(new List<HelperforConfig>() { new HelperforConfig(firstCredit, lastCredit, percentCredit) });
                centreBank.ChangeConfig(currentBank, configCredit);
                break;
            case "Deposit":
                bool flagForDepositConfig = true;
                var listDeposite = new List<HelperforConfig>();
                decimal defaultPercentDeposit = AnsiConsole.Ask<decimal>("Enter the [blue]default[/] percentage on the [blue]Deposit[/] account balance");
                while (flagForDepositConfig)
                {
                    decimal percentDeposit = AnsiConsole.Ask<decimal>("Enter the percentage on the [blue]Deposit[/] account balance");
                    decimal firstDeposit = AnsiConsole.Ask<decimal>("Enter the beginning of the gap on the Deposit account balance");
                    decimal lastDeposit = AnsiConsole.Ask<decimal>("Enter ending of the gap on the Deposit account balance");
                    listDeposite.Add(new HelperforConfig(firstDeposit, lastDeposit, percentDeposit));
                    if (!AnsiConsole.Confirm("Do you want to continue entering gaps for the debit account?"))
                    {
                        flagForDepositConfig = false;
                    }
                }

                var configDeposit = new DepositConfig(listDeposite);
                centreBank.ChangeConfig(currentBank, configDeposit);
                break;
        }
    }
}