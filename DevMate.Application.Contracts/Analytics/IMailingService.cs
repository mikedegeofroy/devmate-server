using ParkingApp.Application.Models;

namespace ParkingApp.Application.Contracts.Analytics;

public interface IMailingService
{
    void Send(TelegramUserModel userModel, string message);
    void Send(TelegramUserModel[] userModel, string message);
}