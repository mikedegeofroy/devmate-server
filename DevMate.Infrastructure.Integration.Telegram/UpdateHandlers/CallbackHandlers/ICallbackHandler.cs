using Telegram.Bot;
using Telegram.Bot.Types;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.CallbackHandlers;

public interface ICallbackHandler
{
    void Handle(ITelegramBotClient botClient, CallbackQuery callbackQuery,
        CancellationToken cancellationToken);

    void SetNext(ICallbackHandler handler);
}