using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.StartParameterHandlers;

public class AttendHandler : IStartParameterHandler
{
    private IStartParameterHandler? _nextHandler;

    public async Task Handle(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        string?[] parts = message.Text.Split(' ');
        string? param = parts.Length > 1 ? parts[1] : null;

        if (param.StartsWith("attend_"))
        {
            string code = param.Split("_")[1];

            var keyboardMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Attend", $"some_callback_data {code}"),
                }
            });

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "This is a event description or something",
                replyMarkup: keyboardMarkup,
                cancellationToken: cancellationToken
            );
        }
    }

    public IStartParameterHandler SetNext(IStartParameterHandler handler)
    {
        _nextHandler = handler;
        return _nextHandler;
    }
}