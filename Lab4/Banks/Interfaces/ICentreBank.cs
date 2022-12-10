using Banks.Entities;
using Banks.Interfaces;
using Banks.Moduls;

namespace Banks.Interfaces;

public interface ICentreBank
{
    Bank AddBank(string name, List<IConfig> configs, decimal percentCommission, decimal defaultPercentDeposit, decimal limitCreditAccount, decimal maxSumIfUserNotTrusted);

    IAccount UserOpenAccount(User user, Bank bank, IAccount account);
    void UserRemoveAccount(User user, Bank bank, IAccount account);
    void ChangeConfig(Bank bank, IConfig config);
    void AccrueInterestCredit();
    void InterestOnBalance();
    void ReplenishInATM(User user, Bank bank, string account, decimal value);
    void TakeOffInATM(User user, Bank bank, string account, decimal money);
    void DisplayMessage(string message) => Console.WriteLine(message);
}