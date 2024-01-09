namespace ParkingApp.Application.Contracts.Analytics;

public interface IAuthService
{
    void Login(string username);
    bool Verification(string verification);

    bool Validate(string username, string token);
}