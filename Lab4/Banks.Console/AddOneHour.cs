using Banks.Services;

namespace Banks.Console;

public class AddOneHour
{
    public static void OneHour(TimeManager timeManager)
    {
        timeManager.AddHour();
    }
}