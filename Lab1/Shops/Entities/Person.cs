using Shop.Exceptions;

namespace Shop.Entities;

public class Person
{
    public Person(string name, decimal money)
    {
        if (money < 0)
        {
            throw new PersonMoneyException("Invalid money person");
        }

        NamePerson = name;
        Money = money;
    }

    public decimal Money { get; set; }
    public string NamePerson { get; }
}