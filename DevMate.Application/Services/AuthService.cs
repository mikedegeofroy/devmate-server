using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevMate.Application.Abstractions.FileSystem;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Telegram;
using DevMate.Application.Services.MediatorHandlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevMate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserCodeRepository _userCodeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IFileSystem _fileSystem;
    private readonly IMediator _mediator;

    public AuthService(
        IUserCodeRepository userCodeRepository,
        IConfiguration configuration,
        IUserRepository userRepository,
        IFileSystem fileSystem,
        IMediator mediator
    )
    {
        _userCodeRepository = userCodeRepository;
        _configuration = configuration;
        _userRepository = userRepository;
        _fileSystem = fileSystem;
        _mediator = mediator;
    }

    public AuthResult Login()
    {
        string code = Guid.NewGuid().ToString("n")[..20];
        return new AuthResult.VerifyCode(code);
    }

    public async Task<AuthResult> ApproveLogin(string? code, TelegramUserDto userDto)
    {
        User? userData = _userRepository.GetUsers().FirstOrDefault(x => x.TelegramId == userDto.TelegramId);

        if (userData != null)
        {
            _userCodeRepository.RegisterUserCode(
                code,
                userData,
                1000
            );
        }
        else
        {
            Stream stream = await _mediator.Send(new DownloadProfilePictureRequest
            {
                TelegramId = userDto.TelegramId
            });

            string name = Guid.NewGuid() + ".jpg";
            string path = await _fileSystem.WriteFile(stream, name);

            _userRepository.AddUser(new User
            {
                Id = -1,
                TelegramId = userDto.TelegramId,
                Username = userDto.Username,
                ProfilePicture = path
            });
        }

        return VerifyLogin(code);
    }

    public AuthResult VerifyLogin(string? code)
    {
        User? user = _userCodeRepository.GetUserByCode(code);
        if (user == null) return new AuthResult.NotFound();


        User client = _userRepository.GetUsers().FirstOrDefault(x => x.TelegramId == user.TelegramId) ??
                      throw new Exception("Something went terribly wrong");

        return new AuthResult.Success(
            new AuthUserDto
            {
                Id = client.Id,
                ProfilePicture = client.ProfilePicture,
                Username = client.Username,
            }, GenerateToken(client));
    }

    private string GenerateToken(User user)
    {
        var secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value!));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}