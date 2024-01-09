namespace ParkingApp.Application.Models;

public class DataPoint
{
    public DataPoint(int value, DateTime dateTime)
    {
        Value = value;
        DateTime = dateTime;
    }

    public int Value { get; }
    public DateTime DateTime { get; }
}