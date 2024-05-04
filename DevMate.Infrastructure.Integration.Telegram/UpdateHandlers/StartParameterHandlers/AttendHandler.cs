using System.Text;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers.StartParameterHandlers;

public class AttendHandler : IStartParameterHandler
{
    private IStartParameterHandler? _nextHandler;
    private IEventRepository _eventRepository;

    public AttendHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

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
            Event? eventById = _eventRepository.GetEventById(Int64.Parse(code));
            var sb = new StringBuilder();
            sb.AppendLine("Event name:");
            sb.AppendLine(eventById.Title);
            sb.AppendLine(String.Empty);
            sb.AppendLine("Event desctiption:");
            sb.AppendLine(eventById.Description);
            sb.AppendLine(String.Empty);
            sb.AppendLine(eventById.StartDateTime.ToString());
            sb.AppendLine(String.Empty);
            sb.AppendLine($"Places: {eventById.Places - eventById.Occupied}/{eventById.Places}");
            // sb.AppendLine("https://devmate.s3.eu-north-1.amazonaws.com/9eafaca9-715b-4ded-86f0-e9ad302750e5.jpg");
            
            // await botClient.SendTextMessageAsync(
            //     chatId: message.Chat.Id,
            //     text: sb.ToString(),
            //     replyMarkup: keyboardMarkup,
            //     cancellationToken: cancellationToken
            // );

            await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new InputFileUrl(
                    eventById.Cover),
                caption: sb.ToString(),
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