using DevMate.Application.Abstractions.Repositories;
using DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.StartParameterHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public class StartHandler : IUpdateHandler
{
    private IUpdateHandler? _updateHandler;
    private IStartParameterHandler _startParameterHandler;
    private IEventRepository _eventRepository;

    public StartHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
        _startParameterHandler = new AuthHandler();
        _startParameterHandler
            .SetNext(new AttendHandler(_eventRepository));
    }

    public async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (
            update.Type == UpdateType.Message
            && update.Message!.Text != null
            && update.Message.Text.StartsWith("/start")
        )
        {
            Message? message = update.Message;
            string?[] parts = message.Text.Split(' ');
            string? param = parts.Length > 1 ? parts[1] : null;

            if (param != null)
                await _startParameterHandler.Handle(botClient, message, cancellationToken);

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