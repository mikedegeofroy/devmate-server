using DevMate.Application.Abstractions.Services;
using DevMate.Application.Abstractions.Telegram;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Event;
using DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using EventHandler = DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.EventHandler;

namespace DevMate.Infrastructure.Integration.Telegram;

public class TelegramBot : ITelegramBot, IEventPublisher
{
    private readonly ILogger<ITelegramBot> _logger;
    private readonly TelegramBotClient _telegramBotClient;

    private readonly IUpdateHandler _updateHandlerChain;

    public TelegramBot(ILogger<ITelegramBot> logger, IConfiguration configuration, IAuthService authService)
    {
        _logger = logger;
        _telegramBotClient = new TelegramBotClient(configuration.GetSection("AppSettings:TelegramBotToken").Value!);

        _updateHandlerChain = new StartHandler();

        _updateHandlerChain
            .SetNext(new EventHandler())
            .SetNext(new InvoiceHandler())
            .SetNext(new CallbackHandler(authService));
    }

    public void Start()
    {
        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: CancellationToken.None
        );

        _logger.LogInformation("Bot is running.");
    }

    private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            _updateHandlerChain.Handle(botClient, update, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred in message handling: {ex.Message}");
        }

        return Task.CompletedTask;
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError($"An error occurred: {exception.Message}");
        return Task.CompletedTask;
    }

    public async void PostEvent(long recipient, EventModel eventModelObject)
    {
        var keyboardMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Find out More", $"more"),
                InlineKeyboardButton.WithCallbackData("Buy", $"buy_{eventModelObject.Id}"),
            }
        });

        await _telegramBotClient.SendPhotoAsync(
            chatId: recipient,
            caption: $"<b>{eventModelObject.Title}<\b>\n{eventModelObject.Description}",
            photo: new InputFileUrl(eventModelObject.Cover),
            replyMarkup: keyboardMarkup,
            cancellationToken: CancellationToken.None
        );
    }
}