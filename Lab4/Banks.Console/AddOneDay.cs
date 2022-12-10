using Banks.Services;

namespace Banks.Console;

public class AddOneDay
{
    public static void OneDay(TimeManager timeManager)
    {
        timeManager.AddDay();
    }
}