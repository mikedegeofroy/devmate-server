namespace DevMate.Application.Models.Analytics;

public class TelegramDataPoint
{
    public TelegramDataPoint(int value, DateTime dateTime)
    {
        Value = value;
        DateTime = dateTime;
    }

    public int Value { get; }
    public DateTime DateTime { get; }
}