using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.StartParameterHandlers;

public class AuthHandler : IStartParameterHandler
{
    private IStartParameterHandler? _nextHandler;

    public async Task Handle(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        string?[] parts = message.Text.Split(' ');
        string? param = parts.Length > 1 ? parts[1] : null;

        if (param.StartsWith("auth_"))
        {
            string code = param.Split("_")[1];

            var keyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Cancel", $"cancel"),
                    InlineKeyboardButton.WithCallbackData("Log In", $"login {code}"),
                }
            });

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "New login request from Saint-Petersburg, Russia.",
                replyMarkup: keyboardMarkup,
                cancellationToken: cancellationToken
            );
        }

        _nextHandler?.Handle(botClient, message, cancellationToken);
    }

    public IStartParameterHandler SetNext(IStartParameterHandler handler)
    {
        _nextHandler = handler;
        return _nextHandler;
    }
}