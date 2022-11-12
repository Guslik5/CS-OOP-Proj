using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    public Person(string name, decimal money)
    {
        ArgumentNullException.ThrowIfNull(name, "Person name is null");
        if (money < 0)
        {
            throw new PersonMoneyException("Invalid money person");
        }

        NamePerson = name;
        Money = money;
    }

    public decimal Money { get; internal set; }
    public string NamePerson { get; }
}