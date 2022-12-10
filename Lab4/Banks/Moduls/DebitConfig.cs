using Banks.Interfaces;

namespace Banks.Moduls;

public class DebitConfig : IConfig
{
    public DebitConfig(List<HelperforConfig> listAmountsAndPercentages)
    {
        ListAmountsAndPercentages = listAmountsAndPercentages;
    }

    public string NameConfig => "Debit";

    public List<HelperforConfig> ListAmountsAndPercentages { get; }
}