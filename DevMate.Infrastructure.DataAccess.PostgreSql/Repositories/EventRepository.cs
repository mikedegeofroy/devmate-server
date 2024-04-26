using System.Data;
using Dapper;
using DevMate.Application.Abstractions.Repositories;
using DevMate.Application.Models.Auth;
using DevMate.Application.Models.Event;
using DevMate.Infrastructure.DataAccess.PostgreSql.DataSources;

namespace DevMate.Infrastructure.DataAccess.PostgreSql.Repositories;

public class EventRepository : IEventRepository
{
    private readonly SqlDataAccess _sql;

    public EventRepository(SqlDataAccess sql)
    {
        _sql = sql;
    }

    public EventModel? GetEventById(long id)
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT * FROM events WHERE id = @Id;
                             """;

        EventModel? eventModel = connection.QueryFirstOrDefault<EventModel>(query, new { Id = id });

        return eventModel;
    }

    public IEnumerable<EventModel> GetEvents()
    {
        IDbConnection connection = _sql.GetConnection();

        const string query = """
                                SELECT * FROM events;
                             """;
        var events = connection.Query<EventModel>(query)
            .Distinct()
            .ToList();

        return events;
    }

    public EventModel AddEvent(User user)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          INSERT INTO events (user_id, title, description, places, occupied, price)
                                          VALUES (@UserId, 'Title', 'Description', 10, 0, 0)
                                          RETURNING *
                                      """;

        EventModel? model = connection.QueryFirstOrDefault<EventModel>(permalinkQuery, new
        {
            UserId = user.Id
        });

        return model;
    }

    public EventModel UpdateEvent(EventModel eventModel)
    {
        IDbConnection connection = _sql.GetConnection();

        const string permalinkQuery = """
                                          UPDATE events SET
                                            user_id = @UserId,
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

        EventModel? model = connection.QueryFirstOrDefault<EventModel>(permalinkQuery, new
        {
            eventModel.Id,
            eventModel.UserId,
            eventModel.Title,
            eventModel.Description,
            eventModel.Places,
            eventModel.Occupied,
            eventModel.Price,
            eventModel.Cover,
            eventModel.StartDateTime,
            eventModel.EndDateTime
        });

        return model;
    }
}