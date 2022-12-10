using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Moduls;

public class User
{
    private List<IAccount> _listAccount = new List<IAccount>();
    public User(string name, string surname, string passport, string address)
    {
        (Name, Surname, Passport, Address) = (name, surname, passport, address);
        GuidUser = Guid.NewGuid();
    }

    public Guid GuidUser { get; }

    public string Name { get; }
    public string Surname { get; }
    public string Passport { get; }
    public string Address { get; }

    public List<IAccount> ListBanksAccounts => _listAccount;
}