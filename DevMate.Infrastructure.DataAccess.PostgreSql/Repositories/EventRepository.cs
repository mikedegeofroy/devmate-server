using System.Data;
using Dapper;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Domain;
using DevMate.Application.Models.Events;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class EventRepository : IEventRepository
{
    private readonly SqlDataAccess _sql;

    public EventRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }

    public Event? GetEventById(long id)
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT * FROM events WHERE id = @Id;
                             """;

        Event? eventModel = connection.QueryFirstOrDefault<Event>(query, new { Id = id });

        return eventModel;
    }

    public IEnumerable<Event> GetEvents()
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT
                                    id,
                                    user_id as UserId, 
                                    user_telegram_id as UserTelegramId, 
                                    title, 
                                    description, 
                                    places, 
                                    occupied, 
                                    price, 
                                    cover, 
                                    end_datetime as EndDateTime, 
                                    start_datetime as StartEndTime 
                                FROM events;
                             """;
        var events = connection.Query<Event>(query)
            .Distinct()
            .ToList();

        return events;
    }

    public Event AddEvent(User user)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          INSERT INTO events (user_id, user_telegram_id, title, description, places, occupied, price)
                                          VALUES (@UserId, @UserTelegramId, 'Title', 'Description', 10, 0, 0)
                                          RETURNING *
                                      """;

        Event? model = connection.QueryFirstOrDefault<Event>(permalinkQuery, new
        {
            UserId = user.Id,
            UserTelegramId = user.TelegramId
        });

        return model;
    }

    public Event UpdateEvent(EventDto toUpdate)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          UPDATE events SET
                                            title = @Title,
                                            description = @Description,
                                            places = @Places,
                                            occupied = @Occupied,
                                            price = @Price,
                                            cover = @Cover,
                                            end_datetime = @EndDatetime,
                                            start_datetime = @StartDatetime
                                                        WHERE id = @Id
                                          RETURNING *
                                      """;

        Event? model = connection.QueryFirstOrDefault<Event>(permalinkQuery, new
        {
            toUpdate.Id,
            toUpdate.Title,
            toUpdate.Description,
            toUpdate.Places,
            toUpdate.Occupied,
            toUpdate.Price,
            toUpdate.Cover,
            toUpdate.StartDateTime,
            toUpdate.EndDateTime
        });

        return model;
    }

    public void AddAttendance(long eventId, long userId, DateTime registrationDatetime)
    {
        IDbConnection connection = _sql.GetConnection();
        
        const string permalinkQuery = """
                                          INSERT INTO event_attendance (user_id, event_id, registration_datetime)
                                          VALUES (@UserId, @EventId, @RegistrationDatetime)
                                          RETURNING *
                                      """;
        connection.QueryFirstOrDefault<Event>(permalinkQuery, new
        {
            UserId = userId,
            EventId = eventId,
            RegistrationDatetime = registrationDatetime
        });
    }
}