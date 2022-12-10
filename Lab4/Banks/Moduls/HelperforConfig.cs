namespace Banks.Moduls;

public class HelperforConfig
{
    public HelperforConfig(decimal first, decimal last, decimal percent)
    {
        (First, Last, Percent) = (first, last, percent);
    }

    public decimal First { get; set; }
    public decimal Last { get; set; }
    public decimal Percent { get; set; }
}