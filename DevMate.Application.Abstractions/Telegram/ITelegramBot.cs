namespace DevMate.Application.Abstractions.Telegram;

public interface ITelegramBot
{
    void Start();

    Task<Stream> DownloadTelegramPicture(long telegramId);
}