using Telegram.Bot;
using Telegram.Bot.Types;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.StartParameterHandlers;

public interface IStartParameterHandler
{
    Task Handle(ITelegramBotClient botClient, Message message,
        CancellationToken cancellationToken);
    
    IStartParameterHandler SetNext(IStartParameterHandler handler);
}