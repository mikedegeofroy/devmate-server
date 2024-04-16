namespace DevMate.Application.Models.Analytics;

public class TelegramAnalyticsData
{
    public TelegramAnalyticsData(TelegramDataPoint[] dataPoints)
    {
        DataPoints = dataPoints;
        Total = dataPoints.Sum(x => x.Value);
    }

    public TelegramDataPoint[] DataPoints { get; set; }
    public int Total { get; }
}