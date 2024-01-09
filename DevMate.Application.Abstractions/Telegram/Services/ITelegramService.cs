using ParkingApp.Application.Abstractions.Telegram.Models;
using ParkingApp.Application.Models;

namespace ParkingApp.Application.Abstractions.Telegram.Services;

public interface ITelegramService
{
    Task<IEnumerable<TelegramMessageModel>> GetMessagesByChatIdAsync(long id, DateTime limit);

    Task<IEnumerable<TelegramPeerModel>> GetPeersAsync();

    void Send(TelegramUserModel user, string message);
}