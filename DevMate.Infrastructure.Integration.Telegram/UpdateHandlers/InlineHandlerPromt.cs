using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Event;
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
            IEnumerable<EventModel> allEvents = _eventRepository.GetEvents();
            //in future we will chose available events for current user by userId
            // var available_events = all_events.Where(x => x.UserId == userId);
            var queryResults = new List<InlineQueryResult>();
            int counter = 0;

            foreach (EventModel availableEvent in allEvents)
            {
                queryResults.Add(new InlineQueryResultArticle(
                    id: $"{counter}",
                    title: availableEvent.Title,
                    inputMessageContent: new InputTextMessageContent("Added a markup")
                )
                {
                    Description = availableEvent.Description,
                    ReplyMarkup = new InlineKeyboardMarkup(new [] { new InlineKeyboardButton("ahaha"){ Url = "https://pornhub.com"} })
                });

                ++counter;
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
}