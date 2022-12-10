using Banks.Services;
using Spectre.Console;
namespace Banks.Console;

public class Program
{
    public static void Main()
    {
        var centreBank = new CentreBank();
        var timeManager = new TimeManager(centreBank);
        var panel = new Panel(new Markup("Welcome to the bank system", Style.Plain.Foreground(Color.Aqua).Decoration(Decoration.SlowBlink)));
        panel.Padding = new Padding(75, 2, 75, 2);
        AnsiConsole.Write(panel);
        Table table = new Table().Centered();

        AnsiConsole.Live(table)
            .Start(ctx =>
            {
                table.AddColumn("[blue]Hi! I'm a banking system, while I'm loading, I'll tell you what I can do[/]");
                ctx.Refresh();
                Thread.Sleep(1000);

                table.AddRow("[blue]1) I can create banks[/]");
                ctx.Refresh();
                Thread.Sleep(1500);
                table.AddRow("[blue]2) I can create accounts for users with money :)[/]");
                ctx.Refresh();
                Thread.Sleep(1500);
                table.AddRow("[blue]3) I can transfer money from one user to another and even withdraw them and add[/]");
                ctx.Refresh();
                Thread.Sleep(1500);
                table.AddRow("[blue]4) And I also know how to move time, if you really want it :D[/]");
                ctx.Refresh();
                Thread.Sleep(1500);
                table.AddRow("[blue] I NEED A LITTLE MORE TIME AND WE CAN START[/]");
                ctx.Refresh();
                Thread.Sleep(1500);
            });
        AnsiConsole.Progress()
            .Start(ctx =>
            {
                // Define tasks
                ProgressTask task1 = ctx.AddTask("[green]Loading Bank System[/]");
                while (!ctx.IsFinished)
                {
                    task1.Increment(0.00001);
                }
            });
        bool flag = true;
        while (flag)
        {
            var panelWithDate = new Panel("Date and time in system" + Environment.NewLine + "Time: " +
                                          centreBank.TimeInSystem.Hour + ":" + centreBank.TimeInSystem.Minute + ":" +
                                          centreBank.TimeInSystem.Second + Environment.NewLine + "Date: " +
                                          centreBank.TimeInSystem.Day + "." + centreBank.TimeInSystem.Month + "." +
                                          centreBank.TimeInSystem.Year);
            AnsiConsole.Write(panelWithDate);
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(new string(Environment.NewLine + "[bold yellow]Main Menu[/]"))
                    .PageSize(12)
                    .MoreChoicesText("[grey](Move up and down to reveal more command)[/]")
                    .AddChoices(new[]
                    {
                        "Create new Bank", "User Open Account", "User Remove Account",
                        "Change Config", "Replenish in ATM", "TakeOff in ATM", "Transaction", "Info about banks", "Info about users", "Add one hour", "Add one day", "Exit",
                    }));
            switch (command)
            {
                case "Create new Bank":
                    CreateBank.CreatingBank(centreBank);
                    break;
                case "User Open Account":
                    UserOpenAccount.UserOpeningAccount(centreBank);
                    break;
                case "User Remove Account":
                    UserRemoveAccount.UserRemovingAccount(centreBank);
                    break;
                case "Change Config":
                    ChangeConfig.ChangingConfig(centreBank);
                    break;
                case "Replenish in ATM":
                    ReplenishInATM.Replenish(centreBank);
                    break;
                case "TakeOff in ATM":
                    TakeOffInATM.TakeOff(centreBank);
                    break;
                case "Info about banks":
                    InfoBanks.InfoAboutBanks(centreBank);
                    break;
                case "Info about users":
                    InfoUsers.InfoAboutUsers(centreBank);
                    break;
                case "Transaction":
                    Transaction.TransactionOnn(centreBank);
                    break;
                case "Add one hour":
                    AnsiConsole.Status()
                        .Start("Wow, you want to move one hour", ctx =>
                        {
                            Thread.Sleep(2500);

                            ctx.Status("Moving in time");
                            ctx.Spinner(Spinner.Known.Star);
                            ctx.SpinnerStyle(Style.Parse("green"));
                            Thread.Sleep(4000);
                            AnsiConsole.MarkupLine("[green]We time - traveled :)[/]");
                        });
                    AddOneHour.OneHour(timeManager);
                    break;
                case "Add one day":
                    AnsiConsole.Status()
                        .Start("Wow, you want to move day hour", ctx =>
                        {
                            Thread.Sleep(2500);

                            ctx.Status("Moving in time");
                            ctx.Spinner(Spinner.Known.Star);
                            ctx.SpinnerStyle(Style.Parse("green"));
                            Thread.Sleep(4000);
                            AnsiConsole.MarkupLine("[green]We time - traveled :)[/]");
                        });
                    AddOneDay.OneDay(timeManager);
                    break;
                case "Exit":
                    flag = false;
                    AnsiConsole.Write(new FigletText("Bye bye").Centered().Color(Color.Aqua));
                    AnsiConsole.Write(new FigletText("I'll be glad to see you again").Centered().Color(Color.Aqua));
                    break;
            }
        }
    }
}