using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Moduls;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class TestBank
{
    [Fact]
    public void Test()
    {
        var helperConfig1 = new HelperforConfig(0, 1000, 1);
        var helperConfig2 = new HelperforConfig(1000, 2000, 2);
        var helperConfig3 = new HelperforConfig(2000, 3000, 3);
        var listConfig = new List<IConfig>();
        var configCredit = new CreditConfig(new List<HelperforConfig>() { helperConfig1, helperConfig2, helperConfig3 });
        var configDebit = new DebitConfig(new List<HelperforConfig>() { helperConfig1, helperConfig2, helperConfig3 });
        var configDeposit = new DepositConfig(new List<HelperforConfig>() { helperConfig1, helperConfig2, helperConfig3 });

        var centreBankSevice = new CentreBank();
        var bank = centreBankSevice.AddBank("Sber", new List<IConfig>() { configCredit, configDebit, configDeposit }, 10, 5, 1, 1000);

        var user1 = new UserBuilder().WithName("Dima").WithSurname("Gusachenko").Build();
        var account1 = centreBankSevice.UserOpenAccount(user1, bank, new DebitAccount());
        centreBankSevice.UserOpenAccount(user1, bank, new CreditAccount());
        centreBankSevice.ReplenishInATM(user1, bank, "Debit", 1500);

        var user2 = new UserBuilder().WithName("Dima").WithSurname("Gusachenko").Build();
        var account2 = centreBankSevice.UserOpenAccount(user2, bank, new DebitAccount());
        centreBankSevice.UserOpenAccount(user2, bank, new CreditAccount());
        centreBankSevice.ReplenishInATM(user2, bank, "Debit", 1500);
        centreBankSevice.TakeOffInATM(user2, bank, "Credit", 1000);
        centreBankSevice.AccrueInterestCredit();

        CentreBank.Transaction(account1, account2, 1000);
        Assert.Equal(400, account1.Money);
        Assert.Equal(2500, account2.Money);
    }
}