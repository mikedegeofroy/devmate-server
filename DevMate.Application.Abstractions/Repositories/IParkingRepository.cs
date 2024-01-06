using ParkingApp.Application.Models;

namespace ParkingApp.Application.Abstractions.Repositories;

public interface IParkingRepository
{
    IEnumerable<DataPoint> GetAllParkings();
}