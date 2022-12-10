using Banks.Commands;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Moduls;

namespace Banks.Services;

public class CentreBank : ICentreBank
{
    private readonly List<Bank> _banks = new List<Bank>();
    private DateTime _timeInSystem = DateTime.Now;

    public delegate void BankHandler(string message);
    public event BankHandler Notify;
    public List<Bank> Banks => _banks;
    public DateTime TimeInSystem
    {
        get
        {
            return _timeInSystem;
        }

        set
        {
            _timeInSystem = value;
            if (_timeInSystem.Hour == 0)
            {
                InterestOnBalance();
            }

            if (_timeInSystem.Day == 1 && _timeInSystem.Hour == 0)
            {
                AccrueInterestCredit();
            }
        }
    }

    public static bool Transaction(IAccount accountFrom, IAccount accountTo, decimal money)
    {
        ArgumentNullException.ThrowIfNull(accountFrom);
        ArgumentNullException.ThrowIfNull(accountTo);
        var context = new Context(accountFrom, accountTo, money);
        var takeOff = new Handler(new TakeOffCommand(context));
        var commission = new Handler(new CommissionCommand(context));
        var replenish = new Handler(new ReplenishCommand(context));
        return new Chain(takeOff, commission, replenish).Execute();
    }

    public static bool Transaction(User userFrom, string bankFrom, string userFromAccountName, User userTo, string bankTo, string userToAccountName, decimal money)
    {
        if (string.IsNullOrEmpty(bankFrom) || string.IsNullOrEmpty(bankTo))
        {
            throw new BankNameForTransactionException("Bank name is null or empty");
        }

        var currentAccountFrom = userFrom.ListBanksAccounts
            .FirstOrDefault(l => l.NameAccount.Equals(userFromAccountName) && l.Bank.Name.Equals(bankFrom));
        var currentAccountTo = userTo.ListBanksAccounts
            .FirstOrDefault(l => l.NameAccount.Equals(userToAccountName) && l.Bank.Name.Equals(bankTo));
        if (currentAccountFrom is null || currentAccountTo is null)
        {
            throw new AccountForTransactionException("Account is null");
        }

        if ((userFrom.Passport is null || userFrom.Address is null) & money > currentAccountFrom.Bank.MaxSumIfUserNotTrusted)
        {
            throw new UntrustedUserException("Untrusted user is trying to instill a limit");
        }

        ArgumentNullException.ThrowIfNull(currentAccountFrom);
        ArgumentNullException.ThrowIfNull(currentAccountTo);
        var context = new Context(currentAccountFrom, currentAccountTo, money);
        var takeOff = new Handler(new TakeOffCommand(context));
        var commission = new Handler(new CommissionCommand(context));
        var replenish = new Handler(new ReplenishCommand(context));
        return new Chain(takeOff, commission, replenish).Execute();
    }

    public Bank AddBank(string name, List<IConfig> configs, decimal percentCommission, decimal defaultPercentDeposit, decimal limitCreditAccount, decimal maxSumIfUserNotTrusted)
    {
        ArgumentNullException.ThrowIfNull(configs);
        if (_banks.Where(b => b.Name.Equals(name)).Any())
        {
            throw new BankCreatedException("Bank Created");
        }

        var bank = new Bank(name, configs, percentCommission, defaultPercentDeposit, limitCreditAccount, maxSumIfUserNotTrusted);
        _banks.Add(bank);
        return bank;
    }

    public IAccount UserOpenAccount(User user, Bank bank, IAccount account)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(account);
        if (user.ListBanksAccounts.Where(a => a.Bank.Name.Equals(bank.Name) && a.Config.NameConfig.Equals(account.NameAccount)).Any())
        {
            throw new UserSubscribeToBankException("This user has already open account in this bank");
        }

        if (!bank.ListUsers.Contains(user))
        {
            bank.ListUsers.Add(user);
        }

        account.Bank = bank;
        account.Config = bank.Configs.Where(c => c.NameConfig.Equals(account.NameAccount)).First();
        user.ListBanksAccounts.Add(account);
        return account;
    }

    public void UserRemoveAccount(User user, Bank bank, IAccount account)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(account);
        var correntAccount = user.ListBanksAccounts
            .FirstOrDefault(a => a.Bank.Name.Equals(bank.Name) && a.Config.NameConfig.Equals(account.NameAccount));

        if (correntAccount is null)
        {
            throw new UserSubscribeToBankException("This user hasn't this account in this bank");
        }

        if (correntAccount.Money != 0)
        {
            throw new AccountRemoveWithMoneyException("There are money on the account");
        }

        bank.ListUsers.Remove(user);
        user.ListBanksAccounts.Remove(correntAccount);
    }

    public void ChangeConfig(Bank bank, IConfig config)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(config);
        var currentConfig = bank.Configs.Where(c => c.NameConfig.Equals(config.NameConfig)).FirstOrDefault();
        if (currentConfig is null)
        {
            throw new ConfigException("Config for change wasn't found");
        }

        bank.Configs.Remove(currentConfig);
        bank.Configs.Add(config);
        this.Notify += DisplayMessage;
        Notify?.Invoke($"{config.NameConfig} Config Changed in {bank.Name}");
    }

    public void AccrueInterestCredit()
    {
        var listAllAccount = _banks.SelectMany(u => u.ListUsers).ToList()
            .SelectMany(a => a.ListBanksAccounts
                .Where(account => account.NameAccount.Equals("Credit") && account.Money < 0))
                    .ToList();

        foreach (IAccount account in listAllAccount)
        {
            foreach (HelperforConfig helper in account.Config.ListAmountsAndPercentages)
            {
                if ((-1 * account.Money) >= helper.First & account.Money < helper.Last)
                {
                    account.TakeOff(account.Money * (helper.Percent / 100 / 12));
                }
            }
        }
    }

    public void InterestOnBalance()
    {
        var listAllAccount = _banks.SelectMany(u => u.ListUsers)
            .ToList().SelectMany(a => a.ListBanksAccounts
            .Where(account => account.NameAccount.Equals("Debit") || account.NameAccount.Equals("Deposit")))
            .ToList();
        var listAccountWithoutHelper = new List<IAccount>();
        foreach (IAccount account in listAllAccount)
        {
            bool flag = true;
            foreach (var helper in account.Config.ListAmountsAndPercentages)
            {
                if (account.Money >= helper.First && account.Money < helper.Last)
                {
                    account.Replenish(account.Money * (helper.Percent / 100 / 365));
                    flag = false;
                }
            }

            if (flag)
            {
                listAccountWithoutHelper.Add(account);
            }
        }

        foreach (IAccount account in listAccountWithoutHelper)
        {
            account.Replenish(account.Money * (account.Bank.DefaultPercentDeposite / 100));
        }
    }

    public void ReplenishInATM(User user, Bank bank, string account, decimal value)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(bank);
        var correntAccount = user.ListBanksAccounts
            .FirstOrDefault(a => a.Bank.Name.Equals(bank.Name) && a.NameAccount.Equals(account));

        if (correntAccount is null)
        {
            throw new AccountException("Account isn't found");
        }

        correntAccount.Replenish(value);
    }

    public void TakeOffInATM(User user, Bank bank, string account, decimal money)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(bank);
        IAccount currentAccount = user.ListBanksAccounts
            .FirstOrDefault(a => a.Bank.Name.Equals(bank.Name) && a.NameAccount.Equals(account));
        if (currentAccount is null)
        {
            throw new AccountException("Account isn't found");
        }

        if ((user.Passport is null || user.Address is null) & money > currentAccount.Bank.MaxSumIfUserNotTrusted)
        {
            throw new UntrustedUserException("Untrusted user is trying to instill a limit");
        }

        if (currentAccount.Money < money && currentAccount.NameAccount != "Credit")
        {
            throw new AccountException("Insufficient funds");
        }

        if (currentAccount.NameAccount == "Credit" & currentAccount.Money - money > bank.LimitCreditAccount)
        {
            throw new AccountException("You have exceeded the limit on your credit account");
        }

        currentAccount.TakeOff(money);
    }

    public void DisplayMessage(string message) => Console.WriteLine(message);
}