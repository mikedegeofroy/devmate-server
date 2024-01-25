using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Telegram.Services;
using DevMate.Application.Contracts.Auth;
using DevMate.Application.Models.Analytics;
using DevMate.Application.Models.Auth;
using DevMate.Infrastructure.Integration.Telegram.Services;
using Microsoft.IdentityModel.Tokens;

namespace DevMate.Application.Auth;

public class TelegramAuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITelegramClientService _telegramClientService;
    private static readonly Dictionary<string, TelegramClientService> Clients = new();

    public TelegramAuthService(IUserRepository userRepository, ITelegramClientService telegramClientService)
    {
        _userRepository = userRepository;
        _telegramClientService = telegramClientService;
    }

    public AuthResult Login(string phone)
    {
        Stream store = _userRepository.GetStore(phone);
        Clients[phone] = new TelegramClientService(phone, store.Length > 0 ? new MemoryStream() : store);

        while (Clients[phone].Requests.Count == 0)
        {
        }

        if (Clients[phone].Requests.Contains("verification_code"))
            return new AuthResult.RequestCode();
        return new AuthResult.NotFound();
    }

    public async Task<AuthResult> VerifyLoginCode(string phone, string code)
    {
        Clients[phone].VerificationCode(code);

        while (Clients[phone].Requests.Count == 0)
        {
        }

        if (Clients[phone].Requests.Contains("verification_password"))
            return new AuthResult.RequestPassword();

        Stream? stream = Clients[phone].Store;
        if (stream != null)
        {
            TelegramUserModel user = await _telegramClientService.GetUser(phone, stream);
            return new AuthResult.Success(new UserDto(phone, user.Photo, user.Username, CreateToken(phone)));
        }

        Clients[phone].Dispose();
        return new AuthResult.NotFound();
    }

    public async Task<AuthResult> VerifyPassword(string phone, string password)
    {
        Clients[phone].VerificationPassword(password);

        if (Clients[phone].Requests.Count == 0)
        {
            Stream? stream = Clients[phone].Store;
            if (stream != null)
            {
                TelegramUserModel user = await _telegramClientService.GetUser(phone, stream);
                Clients[phone].Dispose();
                return new AuthResult.Success(new UserDto(phone, user.Photo, user.Username, CreateToken(phone)));
            }
        }

        Clients[phone].Dispose();
        return new AuthResult.InvalidPassword();
    }

    private string CreateToken(string username)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("my super duper secret key holy shit goddamn fuck off already common"));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(14),
            signingCredentials: cred
        );
        string? token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}