using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevMate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserCodeRepository _userCodeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserCodeRepository userCodeRepository, IConfiguration configuration,
        IUserRepository userRepository)
    {
        _userCodeRepository = userCodeRepository;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public AuthResult Login()
    {
        string code = Guid.NewGuid().ToString("n")[..20];
        return new AuthResult.VerifyCode(code);
    }

    public AuthResult ApproveLogin(string? code, User user)
    {
        _userCodeRepository.RegisterUserCode(
            code,
            user,
            1000
        );

        return VerifyLogin(code);
    }

    public AuthResult VerifyLogin(string? code)
    {
        User? user = _userCodeRepository.GetUserByCode(code);
        if (user == null) return new AuthResult.NotFound();

        IEnumerable<User> what = _userRepository.GetUsers();


        User client = _userRepository.GetUsers().FirstOrDefault(x => x.UserId == user.UserId) ??
                      _userRepository.AddUser(user);

        return new AuthResult.Success(
            new UserDto(
                client.Id,
                "",
                client.Username,
                GenerateToken(client)
            ));
    }

    private string GenerateToken(User user)
    {
        var secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value!));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Sid, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}