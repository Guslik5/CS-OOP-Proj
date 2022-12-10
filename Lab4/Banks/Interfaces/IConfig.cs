using Banks.Moduls;

namespace Banks.Interfaces;

public interface IConfig
{
    string NameConfig { get; }
    public List<HelperforConfig> ListAmountsAndPercentages { get; }
}