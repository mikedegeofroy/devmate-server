using Telegram.Bot;
using Telegram.Bot.Types;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public interface IUpdateHandler
{
    void Handle(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken);
    
    IUpdateHandler SetNext(IUpdateHandler handler);
}