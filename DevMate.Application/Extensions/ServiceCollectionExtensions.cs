using DevMate.Application.Contracts;
using DevMate.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevMate.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddSingleton<IAuthService, AuthService>();
        collection.Decorate<IAuthService, AuthServiceAuthorizedProxy>();
        collection.AddScoped<IEventService, EventService>();
        collection.AddScoped<IWaitlistService, WaitlistService>();
        collection.AddScoped<IUserService, UserService>();

        return collection;
    }
}