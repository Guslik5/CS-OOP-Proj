using Banks.Interfaces;

namespace Banks.Moduls;

public class CreditConfig : IConfig
{
    public CreditConfig(List<HelperforConfig> listAmountsAndPercentages)
    {
        ListAmountsAndPercentages = listAmountsAndPercentages;
    }

    public string NameConfig => "Credit";

    public List<HelperforConfig> ListAmountsAndPercentages { get; }
}