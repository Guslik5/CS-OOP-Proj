using Banks.Moduls;

namespace Banks.Interfaces;

public interface IConfig
{
    string NameConfig { get; }
    List<HelperforConfig> ListAmountsAndPercentages { get; }
}