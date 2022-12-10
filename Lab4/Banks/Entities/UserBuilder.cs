using Banks.Exceptions;
using Banks.Moduls;

namespace Banks.Entities;

public class UserBuilder
{
    private string _name;
    private string _surname;
    private string _passport;
    private string _address;

    public UserBuilder()
    {
        _name = null;
        _surname = null;
        _passport = null;
        _address = null;
    }

    public UserBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public UserBuilder WithSurname(string surname)
    {
        _surname = surname;
        return this;
    }

    public UserBuilder WithPassport(string passport)
    {
        _passport = passport;
        return this;
    }

    public UserBuilder WithAdress(string address)
    {
        _address = address;
        return this;
    }

    public User Build()
    {
        return new User(
            _name ?? throw new UserException("Invalid user name"), _surname ?? throw new UserException("Invalid user surname"), _passport, _address);
    }
}