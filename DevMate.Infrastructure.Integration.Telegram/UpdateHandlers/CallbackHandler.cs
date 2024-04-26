using DevMate.Application.Contracts;
using DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.CallbackHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public class CallbackHandler : IUpdateHandler
{
    private IUpdateHandler? _updateHandler;
    private readonly ICallbackHandler _callbackHandler;

    public CallbackHandler(IAuthService authService)
    {
        _callbackHandler = new LoginHandler(authService);
    }

    public void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update is { Type: UpdateType.CallbackQuery, CallbackQuery: not null })
        {
            _callbackHandler.Handle(botClient, update.CallbackQuery, cancellationToken);
            return;
        }

        _updateHandler?.Handle(botClient, update, cancellationToken);
    }

    public IUpdateHandler SetNext(IUpdateHandler handler)
    {
        _updateHandler = handler;
        return _updateHandler;
    }
}