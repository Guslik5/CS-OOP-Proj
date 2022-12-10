using Banks.Entities;
using Banks.Moduls;
using Banks.Services;
using Spectre.Console;

namespace Banks.Console;

public class InfoBanks
{
    public static void InfoAboutBanks(CentreBank centreBank)
    {
        var table = new Table();
        table.AddColumn("Banks");
        table.AddColumn(new TableColumn("DebitConfig").Centered());
        table.AddColumn(new TableColumn("CreditConfig").Centered());
        table.AddColumn(new TableColumn("DepositConfig").Centered());
        table.AddColumn(new TableColumn("Another info").Centered());

        foreach (Bank bank in centreBank.Banks)
        {
            var helperDebit = centreBank.Banks.Where(b => b.Name.Equals(bank.Name))
                .SelectMany(b => b.Configs)
                    .Where(c => c.NameConfig.Equals("Debit"))
                        .SelectMany(c => c.ListAmountsAndPercentages).FirstOrDefault();
            var panelForConfigDebit = new Panel("Interest rate for any amount: " + helperDebit.Percent.ToString() + "%");
            panelForConfigDebit.Header = new PanelHeader("Debit");

            var helperCredit = centreBank.Banks.Where(b => b.Name.Equals(bank.Name))
                .SelectMany(b => b.Configs)
                    .Where(c => c.NameConfig.Equals("Credit"))
                        .SelectMany(c => c.ListAmountsAndPercentages).FirstOrDefault();
            var panelForConfigCredit = new Panel("The interest rate on a negative account balance: " + helperCredit.Percent.ToString() + "%");
            panelForConfigCredit.Header = new PanelHeader("Credit");

            var helperDeposit = centreBank.Banks.Where(b => b.Name.Equals(bank.Name))
                .SelectMany(b => b.Configs)
                    .Where(c => c.NameConfig.Equals("Deposit"))
                        .SelectMany(c => c.ListAmountsAndPercentages).ToList();
            string stringDeposit = "Default percent is " + centreBank.Banks.Where(b => b.Name.Equals(bank.Name)).First().DefaultPercentDeposite + System.Environment.NewLine;
            foreach (HelperforConfig helper in helperDeposit)
            {
                stringDeposit += "Beginning of the gap: " + helper.First.ToString() + "; End of the gap: " + helper.Last.ToString() + "; Interest rate: " + helper.Percent.ToString() + "%" + System.Environment.NewLine;
            }

            var panelForConfigDeposit = new Panel(stringDeposit);
            panelForConfigDeposit.Header = new PanelHeader("Deposit");
            string stringInfo = "Percent Comission: " + bank.PercentComission.ToString() + System.Environment.NewLine +
                                "Default percent for deposit: " + bank.DefaultPercentDeposite.ToString() + System.Environment.NewLine +
                                "Limit Credit Account: " + bank.LimitCreditAccount.ToString() + System.Environment.NewLine +
                                "Max Sum If User Not Trusted: " + bank.MaxSumIfUserNotTrusted.ToString() + System.Environment.NewLine;
            var panelForInfo = new Panel(stringInfo);
            table.AddRow(new Markup(bank.Name), panelForConfigDebit, panelForConfigCredit, panelForConfigDeposit, panelForInfo);
        }

        AnsiConsole.Write(table);
    }
}