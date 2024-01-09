using DevMate.Application.Models.Analytics;

namespace DevMate.Application.Contracts.Analytics;

public interface IUrlAnalyticsService
{
    string GenerateShortUrl();
    Task<UrlAnalyticsData> GetUrlData();
}