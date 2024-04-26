using DevMate.Application.Contracts;
using DevMate.Application.Models.Auth;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = DevMate.Application.Models.Auth.User;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.CallbackHandlers;

public class LoginHandler : ICallbackHandler
{
    private readonly IAuthService _authService;
    private ICallbackHandler? _nextHandler;

    public LoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async void Handle(ITelegramBotClient botClient, CallbackQuery callbackQuery,
        CancellationToken cancellationToken)
    {
        string callbackData = callbackQuery.Data ?? "";
        if (!callbackData.StartsWith("login"))
        {
            _nextHandler?.Handle(botClient, callbackQuery, cancellationToken);
            return;
        }

        string?[] parts = callbackData.Split(' ');
        string? param = parts.Length > 1 ? parts[1] : null;

        if (param != null)
        {
            if (callbackQuery.From.Username == null) return;

            AuthResult result = _authService.ApproveLogin(param,
                new User
                {
                    UserId = callbackQuery.From.Id,
                    Username = callbackQuery.From.Username,
                    Id = -1
                }
            );


            // Edit the message and remove the keyboard after successful login
            if (result is AuthResult.Success success)
            {
                await botClient.EditMessageTextAsync(
                    chatId: callbackQuery.Message!.Chat.Id,
                    messageId: callbackQuery.Message.MessageId,
                    text: $"You have successfully logged in as {success.User.Username}",
                    replyMarkup: null, // This removes the keyboard
                    cancellationToken: cancellationToken
                );
            }
        }
        else
        {
            // Optionally, keep the keyboard and edit the message to prompt for correct input
            await botClient.EditMessageTextAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                messageId: callbackQuery.Message.MessageId,
                text: "Login ID is missing. Please send /start followed by your ID.",
                replyMarkup: callbackQuery.Message.ReplyMarkup, // Keep the existing keyboard
                cancellationToken: cancellationToken
            );
        }
    }

    public void SetNext(ICallbackHandler handler)
    {
        _nextHandler = handler;
    }
}