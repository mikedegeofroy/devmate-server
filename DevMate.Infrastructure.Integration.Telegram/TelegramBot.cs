using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Abstractions.Services;
using DevMate.Application.Abstractions.Telegram;
using DevMate.Application.Contracts;
using DevMate.Application.Models.Domain;
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
    private readonly IEventRepository _eventRepository;

    private readonly IUpdateHandler _updateHandlerChain;

    public TelegramBot(ILogger<ITelegramBot> logger, IConfiguration configuration, IAuthService authService, IEventRepository eventRepository)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _telegramBotClient = new TelegramBotClient(configuration.GetSection("AppSettings:TelegramBotToken").Value!);

        _updateHandlerChain = new StartHandler(_eventRepository);

        _updateHandlerChain
            .SetNext(new InlineHandlerPromt(_eventRepository))
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

    public async Task<Stream> DownloadTelegramPicture(long telegramId)
    {
        UserProfilePhotos photos = await _telegramBotClient.GetUserProfilePhotosAsync(telegramId, limit: 1);
        
        PhotoSize photo = photos.Photos[0][^1];

        var fileStream = new MemoryStream();
        
        await _telegramBotClient.GetInfoAndDownloadFileAsync(photo.FileId, fileStream);

        return fileStream;
    }

    private Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Received new update {update.Type}");
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

    public async void PostEvent(long recipient, Event eventObject)
    {
        var keyboardMarkup = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Find out More", $"more"),
                InlineKeyboardButton.WithCallbackData("Buy", $"buy_{eventObject.Id}"),
            }
        });

        await _telegramBotClient.SendPhotoAsync(
            chatId: recipient,
            caption: $"<b>{eventObject.Title}<\b>\n{eventObject.Description}",
            photo: new InputFileUrl(eventObject.Cover),
            replyMarkup: keyboardMarkup,
            cancellationToken: CancellationToken.None
        );
    }
}