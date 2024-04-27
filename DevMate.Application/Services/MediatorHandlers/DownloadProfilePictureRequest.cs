using DevMate.Application.Abstractions.Telegram;
using MediatR;

namespace DevMate.Application.Services.MediatorHandlers;

public class DownloadProfilePictureRequest : IRequest<Stream>
{
    public long TelegramId { get; set; }
}