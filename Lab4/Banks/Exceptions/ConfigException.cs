namespace Banks.Exceptions;

public class ConfigException : Exception
{
    public ConfigException(string messege)
        : base(messege) { }
}