using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Contracts.Auth;
using DevMate.Application.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevMate.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserCodeRepository _userCodeRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserCodeRepository userCodeRepository, IConfiguration configuration)
    {
        _userCodeRepository = userCodeRepository;
        _configuration = configuration;
    }

    public AuthResult Login()
    {
        string code = Guid.NewGuid().ToString("n")[..20];
        return new AuthResult.VerifyCode(code);
    }

    public AuthResult ApproveLogin(string? code, User userDto)
    {
        Console.WriteLine(userDto.Id);

        _userCodeRepository.RegisterUserCode(
            code,
            new User(userDto.Id, userDto.Username),
            1000
        );

        return VerifyLogin(code);
    }

    public AuthResult VerifyLogin(string? code)
    {
        User? user = _userCodeRepository.GetUserByCode(code);

        return user != null
            ? new AuthResult.Success(
                new UserDto(
                    user.Id,
                    "",
                    user.Username,
                    GenerateToken(user)
                ))
            : new AuthResult.NotFound();
    }

    private string GenerateToken(User user)
    {
        var secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JwtToken").Value!));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Username),
        };

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}