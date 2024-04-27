using DevMate.Application.Abstractions.Telegram;
using MediatR;

namespace DevMate.Application.Services.MediatorHandlers;


public class DownloadProfilePictureHandler : IRequestHandler<DownloadProfilePictureRequest, Stream>
{
    private readonly ITelegramBot _telegramBot;

    public DownloadProfilePictureHandler(ITelegramBot telegramBot)
    {
        _telegramBot = telegramBot;
    }

    public Task<Stream> Handle(DownloadProfilePictureRequest request, CancellationToken cancellationToken)
    {
        return _telegramBot.DownloadTelegramPicture(request.TelegramId);
    }
}