using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using TL;
using WTelegram;

namespace DevMate.Infrastructure.Integration.Telegram.Services;

public class TelegramClientService : ITelegramClientService, IDisposable
{
    private readonly TaskCompletionSource<string?> _phoneNumber = new();
    private readonly TaskCompletionSource<string?> _verificationCode = new();
    private readonly TaskCompletionSource<string?> _password = new();
    private readonly Client _client;
    private readonly IFileSystem _fileSystem;
    private readonly IUserRepository _userRepository;
    public Stream? Store { get; private set; }

    public TelegramClientService(IFileSystem fileSystem, IUserRepository userRepository, string phoneNumber, Stream? store)
    {
        Requests = new List<string>();
        _phoneNumber.SetResult(phoneNumber);
        _client = new Client(Config(), store);
        _client.LoginUserIfNeeded();
        _fileSystem = fileSystem;
        _userRepository = userRepository;
        Store = store;
    }

    public IList<string> Requests { get; private set; }

    public void VerificationCode(string code)
    {
        _verificationCode.SetResult(code);
        Requests.Remove("verification_code");
    }

    public void VerificationPassword(string password)
    {
        _password.SetResult(password);
        Requests.Remove("verification_password");
    }

    private Task<string?> RequestPhoneNumber()
    {
        return _phoneNumber.Task;
    }

    private Task<string?> RequestVerificationCode()
    {
        Requests.Add("verification_code");
        return _verificationCode.Task;
    }

    private Task<string?> RequestPassword()
    {
        Requests.Add("verification_password");
        return _password.Task;
    }

    public async Task<ITelegramClient> Build()
    {
        await _client.LoginUserIfNeeded();
        return new TelegramClient(_fileSystem, _client);
    }

    private Func<string, string?> Config()
    {
        return what =>
        {
            return what switch
            {
                "api_id" => "16731492",
                "api_hash" => "8eef29fcf2db51e82f0a93069cc46ea1",
                "phone_number" => RequestPhoneNumber().GetAwaiter().GetResult(),
                "verification_code" => RequestVerificationCode().GetAwaiter().GetResult(),
                "password" => RequestPassword().GetAwaiter().GetResult(),
                _ => null
            };
        };
    }

    public void Dispose()
    {
        if (Store is MemoryStream)
        {
            _client.Auth_LogOut();
        }

        Store?.Dispose();
        _client.Dispose();
    }
}