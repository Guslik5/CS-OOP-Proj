namespace Banks.Services;

public class TimeManager
{
    private CentreBank _centreBank;
    public TimeManager(CentreBank centreBank)
    {
        _centreBank = centreBank;
    }

    public void AddHour()
    {
        _centreBank.TimeInSystem = _centreBank.TimeInSystem.AddHours(1);
    }

    public void AddDay()
    {
        for (int i = 0; i < 24; i++)
        {
            _centreBank.TimeInSystem = _centreBank.TimeInSystem.AddHours(1);
        }
    }
}