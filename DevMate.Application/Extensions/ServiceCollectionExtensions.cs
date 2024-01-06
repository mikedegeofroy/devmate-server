using Microsoft.Extensions.DependencyInjection;
using ParkingApp.Application.Analytics;
using ParkingApp.Application.Contracts.Analytics;

namespace ParkingApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IAnalyticsService, AnalyticsService>();
        
        return collection;
    }
}