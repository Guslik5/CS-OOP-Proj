using Banks.Interfaces;

namespace Banks.Moduls;

public class DepositConfig : IConfig
{
    public DepositConfig(List<HelperforConfig> listAmountsAndPercentages)
    {
        ListAmountsAndPercentages = listAmountsAndPercentages;
    }

    public string NameConfig => "Deposit";

    public List<HelperforConfig> ListAmountsAndPercentages { get; }
}