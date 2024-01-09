namespace ParkingApp.Application.Models;

public class AnalyticsData
{
    public AnalyticsData(DataPoint[] dataPoints)
    {
        DataPoints = dataPoints;
        Total = dataPoints.Sum(x => x.Value);
    }

    public DataPoint[] DataPoints { get; set; }
    public int Total { get; }
}