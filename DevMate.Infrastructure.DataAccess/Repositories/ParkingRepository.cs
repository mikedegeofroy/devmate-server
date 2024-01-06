using Itmo.Dev.Platform.Postgres.Connection;
using ParkingApp.Application.Abstractions.Repositories;
using ParkingApp.Application.Models;

namespace ParkingApp.Infrastructure.DataAccess.Repositories;

public class ParkingRepository : IParkingRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public ParkingRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public IEnumerable<DataPoint> GetAllParkings()
    {
        return new List<DataPoint>() { new DataPoint(10), new DataPoint(9), new DataPoint(8) };
    }
}