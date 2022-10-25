namespace Shop.Models;

public class Address
{
    public Address(string address)
    {
        NameAddress = address;
    }

    public string NameAddress { get; }
}