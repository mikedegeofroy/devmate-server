using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace DevMate.Infrastructure.Integration.Telegram.UpdateHandlers;

public class InlineHandlerPromt : IUpdateHandler
{
    private readonly IEventRepository _eventRepository;
    private IUpdateHandler? _updateHandler;

    public InlineHandlerPromt(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (
            update.Type == UpdateType.InlineQuery
        )
        {
            IEnumerable<Event> events = _eventRepository.GetEvents();
            // .Where(x => x.UserTelegramId == update.InlineQuery.From.Id);

            var queryResults = new List<InlineQueryResult>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (Event availableEvent in events)
            {
                var replyMarkup = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton("Attend")
                    {
                        Url = $"https://t.me/devm8bot?start=attend_{availableEvent.Id}"
                    }
                });


                queryResults.Add(new InlineQueryResultArticle(
                    id: $"{availableEvent.Id}",
                    title: availableEvent.Title,
                    inputMessageContent: new InputTextMessageContent(
                        $"[ ](https://sightquest.ru)\n" +
                        $"**{EscapeMarkdownV2(availableEvent.Title)}**\n" +
                        $"{EscapeMarkdownV2(availableEvent.Description)}\n"
                    )
                    {
                        ParseMode = ParseMode.MarkdownV2
                    }
                )
                {
                    Description = availableEvent.Description,
                    ReplyMarkup = replyMarkup,
                    ThumbnailUrl = availableEvent.Cover
                });
            }

            await botClient.AnswerInlineQueryAsync(update.InlineQuery?.Id, queryResults,
                cancellationToken: cancellationToken);
            return;
        }

        _updateHandler?.Handle(botClient, update, cancellationToken);
    }


    public IUpdateHandler SetNext(IUpdateHandler handler)
    {
        _updateHandler = handler;
        return _updateHandler;
    }

    public static string EscapeMarkdownV2(string text)
    {
        char[] specialChars =
            { '_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!' };
        return specialChars.Aggregate(text, (current, c) => current.Replace($"{c}", $"\\{c}"));
    }
}