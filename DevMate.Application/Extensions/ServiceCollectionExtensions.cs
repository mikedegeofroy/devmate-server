using DevMate.Application.Contracts;
using DevMate.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<IAuthService, AuthService>();
        collection.AddScoped<IEventService, EventService>();

        return collection;
    }
}