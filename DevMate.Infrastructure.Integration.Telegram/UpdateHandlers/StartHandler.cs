using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public class StartHandler : IUpdateHandler
{
    private IUpdateHandler? _updateHandler;

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

            var keyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel", $"cancel"),
                    InlineKeyboardButton.WithCallbackData("Log In", $"login {param}"),
                }
            });

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: param != null
                    ? "New login request from Saint-Petersburg, Russia."
                    : "Please provide an ID with /start command.",
                replyMarkup: keyboardMarkup,
                cancellationToken: cancellationToken
            );

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