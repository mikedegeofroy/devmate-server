using DevMate.Application.Abstractions.Telegram;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevMate.Infrastructure.Integration.Telegram.Extensions;

public static class HostExtensions
{
    public static IHost RunBot(this IHost app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        ITelegramBot botService = app.Services.GetRequiredService<ITelegramBot>();
        botService.Start();
        return app;
    }
}