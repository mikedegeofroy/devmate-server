using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Abstractions.Telegram.Services.UserClients;
using WTelegram;

namespace DevMate.Infrastructure.Integration.Telegram.User.Services;

public class TelegramUserClientFactory : ITelegramUserClientFactory
{
    private readonly IFileSystem _fileSystem;

    public TelegramUserClientFactory(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public ITelegramUserClient GetClient()
    {
        return new TelegramUserClient(_fileSystem, new Client(Config()));
    }

    private Func<string, string?> Config()
    {
        return what =>
        {
            return what switch
            {
                "api_id" => "16731492",
                "api_hash" => "8eef29fcf2db51e82f0a93069cc46ea1",
                "phone_number" => "+79213877660",
                "verification_code" => Console.ReadLine(),
                "password" => Console.ReadLine(),
                _ => throw new ArgumentOutOfRangeException(nameof(what), what, null)
            };
        };
    }
}